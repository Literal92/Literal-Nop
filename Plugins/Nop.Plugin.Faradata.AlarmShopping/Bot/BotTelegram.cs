using Nop.Plugin.Faradata.AlarmShopping.Domain;
using Nop.Plugin.Faradata.AlarmShopping.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nop.Plugin.Faradata.AlarmShopping.Bot
{
    public class BotTelegram
    {
        public readonly IFaraBotUserService _BotUserService;
        public readonly IFaraBotConfigService _BotConfigService;
        public BotTelegram(IFaraBotUserService BotService, IFaraBotConfigService BotConfigService)
        {
            _BotUserService = BotService;
            _BotConfigService = BotConfigService;
        }

        public Telegram.Bot.TelegramBotClient bot
        {
            get { return bot; }
            set
            {
                var recConfig = _BotConfigService.Get();
                if (recConfig != null)
                {
                    bot = new Telegram.Bot.TelegramBotClient(recConfig.TokenApi);
                }
            }
        }

        //public void SetBot()
        //{
        //    var recConfig = _BotConfigService.Get().FirstOrDefault();
        //    if (recConfig == null)
        //    {
        //        bot = new Telegram.Bot.TelegramBotClient(recConfig.TokenApi);

        //        bot.OnMessage += BotTelegram.Bot_OnMessage;
        //        bot.StartReceiving();
        //    }
        //}



        public void Bot_OnMessage(object sender, Telegram.Bot.Args.MessageEventArgs e)
        {

            string rec = null;

            var recs = GetByChatId(e.Message.Chat.Id);
            if (rec != null)
                SendMessage(e.Message.Chat.Id, "خوش آمدی رفیق");
            else
                SendMessage(e.Message.Chat.Id, "باید ثبت نام کنی جیگر");
        }

        public async Task<Telegram.Bot.Types.Message> SendMessage(long chatId, string text)
        {
            try
            {
                var rec = await bot.SendTextMessageAsync(chatId, text);
                return rec;
            }
            catch (Exception ee)
            {
                return null;
            }
        }

        public FaraBotUser GetByChatId(long ChatId)
        {
            var d = _BotUserService.GetByChatId(ChatId);
            return d;
        }

        //public async Task<Telegram.Bot.Types.Message> SendMessage(Telegram.Bot.TelegramBotClient bot, long chatId, string text)
        //{
        //    try
        //    {
        //        var rec = await bot.SendTextMessageAsync(chatId, text);
        //        return rec;
        //    }
        //    catch (Exception ee)
        //    {
        //        return null;
        //    }
        //}
    }
}
