using System;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using MihaZupan.TelegramBotClients;

namespace Thankies.Bot.Api.Client
{
    public class BotClient : IBotClient
    {
        protected readonly ILogger<BotClient> Logger;
        public RateLimitedTelegramBotClient Client { get; }
        private bool Started { get; }
        public string BotUrl { get; }

        public BotClient(IConfiguration configuration, ILogger<BotClient> logger)
        {
            Logger = logger;
            BotUrl = configuration["BOT_URL"];

            if (!Started)
            {
                try
                {
                    Client = new RateLimitedTelegramBotClient(configuration["TELEGRAM_TOKEN"]);
                
                    var me = Client.GetMeAsync().Result;
                
                    Logger.LogInformation($"Telegram Bot Client configured for bot: @{me.Username}");

                    Started = true;
                }
                catch (Exception e)
                {
                    Logger.LogError(e, "Error setting TelegramBotClient.");
                
                    throw;
                }      
            }
        }
    }
}