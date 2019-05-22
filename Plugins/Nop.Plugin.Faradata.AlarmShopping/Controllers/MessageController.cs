using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Nop.Core;
using Nop.Plugin.Faradata.AlarmShopping.Domain;
using Nop.Plugin.Faradata.AlarmShopping.Services;
using Nop.Services.Catalog;
using Nop.Web.Framework;
using Nop.Web.Framework.Controllers;
using Nop.Web.Framework.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot.Types;
using Nop.Plugin.Faradata.AlarmShopping;
using Nop.Services.Customers;
using Nop.Core.Domain.Customers;

namespace Nop.Plugin.Faradata.AlarmShopping.Controllers
{
    public class MessageController : BasePluginController
    {
        public readonly ICustomerRegistrationService _CustomerService;
        public readonly IFaraBotUserService _BotUserService;
        public readonly IFaraBotConfigService _BotConfigService;
        private readonly IWebHelper _webHelper;
        private readonly CustomerSettings _customerSettings;

        public MessageController(IFaraBotUserService BotService, IWebHelper webHelper, IFaraBotConfigService BotConfigService, ICustomerRegistrationService CustomerService, CustomerSettings customerSettings)
        {
            _BotUserService = BotService;
            _BotConfigService = BotConfigService;
            _CustomerService = CustomerService;
            _customerSettings = customerSettings;
            _webHelper = webHelper;
        }

        [Route("bot/message/update")]
        public IActionResult Get()
        {
            var cnfg = _BotConfigService.Get();
            var url = _webHelper.GetStoreHost(true);
            var Token = "";
            if (cnfg == null /*|| cnfg.IsActive == false*/)
                return Redirect("/Admin/Farabot/Config");

            Token = cnfg.TokenApi;
            Models.Bot.GetBotClientAsync(Token, url).Wait();

            return Redirect("/Admin/Farabot/Config");
        }

        [HttpPost]
        [Route("bot/message/update")]
        public async Task<OkResult> Post([FromBody]Update update)
        {
            if (update == null) return Ok();

            var commands = Models.Bot.Commands;
            var message = update.Message;
            var botClient = await Models.Bot.GetBotClientAsync();
            // SendReport(150);
            foreach (var command in commands)
            {
                if (command.Contains(message))
                {
                    await command.Execute(message, botClient);
                    return Ok();
                }
            }
            var user = _BotUserService.GetByChatId(message.Chat.Id);
            //user.Step = 1;
            //_BotUserService.Update(user);

            if (user != null)
            {
                SendTextByStep(user.Step, update, user);
            }
            else
            {
                _BotUserService.Insert(new FaraBotUser() { ChatId = message.Chat.Id, CreateDate = DateTime.UtcNow, Step = 0 });
                var userNew = _BotUserService.GetByChatId(message.Chat.Id);
                SendTextByStep(user.Step, update, userNew);
            }

            // await botClient.SendTextMessageAsync(message.Chat.Id, "تو بهترینی پاره جونی");

            return Ok();
        }


        public async void SendTextByStep(byte type, Update update, FaraBotUser user = null)
        {
            var bot = await Models.Bot.GetBotClientAsync();
            var chatId = update.Message.Chat.Id;
            switch (type)
            {
                case 0:
                    user.Step = 1;
                    _BotUserService.Update(user);
                    await bot.SendTextMessageAsync(chatId, "نام کاربری خود را وارد نمایید.");
                    break;
                case 1:
                    user.username = update.Message.Text;
                    user.Step = 2;
                    _BotUserService.Update(user);
                    await bot.SendTextMessageAsync(chatId, "رمز عبور خود را وارد نمایید.");
                    break;
                case 2:
                    var loginResult = _CustomerService.ValidateCustomer(_customerSettings.UsernamesEnabled ? user.username : user.username, update.Message.Text);
                    switch (loginResult)
                    {
                        case CustomerLoginResults.Successful:
                            user.Step = 3;
                            _BotUserService.Update(user);
                            await bot.SendTextMessageAsync(chatId, "ثبت اطلاع رسانی برای شما با موفیت انجام شد");
                            break;
                        case CustomerLoginResults.Deleted:
                        case CustomerLoginResults.NotRegistered:
                        case CustomerLoginResults.CustomerNotExist:
                            user.Step = 1;
                            _BotUserService.Update(user);
                            await bot.SendTextMessageAsync(chatId, "نام کاربری شما صحیح نمی باشد.");
                            await bot.SendTextMessageAsync(chatId, "نام کاربری خود را وارد نمایید.");
                            break;
                        case CustomerLoginResults.WrongPassword:
                            user.Step = 1;
                            _BotUserService.Update(user);
                            await bot.SendTextMessageAsync(chatId, "رمز عبور شما صحیح نمی باشد.");
                            await bot.SendTextMessageAsync(chatId, "نام کاربری خود را وارد نمایید.");
                            break;
                        case CustomerLoginResults.LockedOut:
                        case CustomerLoginResults.NotActive:
                            user.Step = 1;
                            _BotUserService.Update(user);
                            await bot.SendTextMessageAsync(chatId, "اکانت وارد شده غیرفعال می باشد.");
                            await bot.SendTextMessageAsync(chatId, "نام کاربری خود را وارد نمایید.");
                            break;
                    }
                    break;
                default:
                    break;
            }
        }

        public async Task<bool> SendReport(int id)
        {
            try
            {
                var bot = await Models.Bot.GetBotClientAsync();

                var List = _BotUserService.Get(3);
                foreach (var item in List)
                {
                    if (item.ChatId > 0)
                    {
                        var msg = string.Format("فاکتور جدیدی با شماره {0}، ثبت شده است لطفا بررسی نمایید.", id);
                        bot.SendTextMessageAsync(item.ChatId, msg);
                    }
                }
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
