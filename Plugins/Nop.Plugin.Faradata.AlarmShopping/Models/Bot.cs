using Newtonsoft.Json;
using Nop.Core;
using Nop.Plugin.Faradata.AlarmShopping.Models.Commands;
using Nop.Plugin.Faradata.AlarmShopping.Services;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Telegram.Bot;

namespace Nop.Plugin.Faradata.AlarmShopping.Models
{
    public class Bot
    {
        private static TelegramBotClient botClient;
        private static List<Command> commandsList;

        public static IReadOnlyList<Command> Commands => commandsList.AsReadOnly();

        public static async Task<TelegramBotClient> GetBotClientAsync(string token="", string url="")
        {
            if (botClient != null)
                return botClient;

            commandsList = new List<Command>();
            commandsList.Add(new StartCommand());
            //TODO: Add more commands

            botClient = new TelegramBotClient(token);
            string hook = string.Format(url + "{0}", "bot/message/update");
            await botClient.DeleteWebhookAsync();
            await botClient.SetWebhookAsync(hook);
            return botClient;
        }
    }
}
