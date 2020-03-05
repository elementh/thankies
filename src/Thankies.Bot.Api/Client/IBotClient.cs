﻿using MihaZupan.TelegramBotClients;

 namespace Thankies.Bot.Api.Client
{
    public interface IBotClient
    {
        RateLimitedTelegramBotClient Client { get; }
        public string BotUrl { get; }
    }
}