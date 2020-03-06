using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.Extensions.Logging;
using Telegram.Bot.Types.InlineQueryResults;
using Thankies.Infrastructure.Contract.Service;

namespace Thankies.Core.Domain
{
    public class ThanksInlineActionHandler : IRequestHandler<ThanksInlineAction, IEnumerable<InlineQueryResultBase>>
    {
        protected readonly ILogger<ThanksInlineActionHandler> Logger;
        protected readonly IGratitudeService GratitudeService;

        public ThanksInlineActionHandler(ILogger<ThanksInlineActionHandler> logger, IGratitudeService gratitudeService)
        {
            Logger = logger;
            GratitudeService = gratitudeService;
        }

        public async Task<IEnumerable<InlineQueryResultBase>> Handle(ThanksInlineAction request, CancellationToken cancellationToken)
        {
            var gratitude = await GratitudeService.Get("James", "leet");

            if (string.IsNullOrWhiteSpace(gratitude))
            {
                throw new Exception();
            }
            return new List<InlineQueryResultBase>()
            {
                new InlineQueryResultArticle("basic", "Basic gratitude", new InputTextMessageContent(gratitude)),
                new InlineQueryResultArticle("mocking", "Mocking gratitude", new InputTextMessageContent(gratitude)),
                new InlineQueryResultArticle("shouting", "Shouting gratitude", new InputTextMessageContent(gratitude)),
                new InlineQueryResultArticle("leet", "Leet gratitude", new InputTextMessageContent(gratitude))
            };
        }
    }
}