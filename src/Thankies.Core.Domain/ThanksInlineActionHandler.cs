using System;
using System.Collections.Generic;
using System.Linq;
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
            var gratitude = await GratitudeService.GetForEveryFilter(request.Name);

            if (gratitude.Count < 4)
            {
                throw new Exception();
            }
            return new List<InlineQueryResultBase>
            {
                new InlineQueryResultArticle(gratitude[0].Item1, "Basic gratitude", new InputTextMessageContent(gratitude[0].Item2)),
                new InlineQueryResultArticle(gratitude[1].Item1, "Mocking gratitude", new InputTextMessageContent(gratitude[1].Item2)),
                new InlineQueryResultArticle(gratitude[2].Item1, "Shouting gratitude", new InputTextMessageContent(gratitude[2].Item2)),
                new InlineQueryResultArticle(gratitude[3].Item1, "Leet gratitude", new InputTextMessageContent(gratitude[3].Item2))
            };
        }
    }
}