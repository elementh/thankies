using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Telegram.Bot.Types.InlineQueryResults;
using Thankies.Infrastructure.Contract.Service;

namespace Thankies.Core.Domain.Inline.ThanksInlineAction
{
    public class ThanksInlineActionHandler : IRequestHandler<ThanksInlineAction, IEnumerable<InlineQueryResultBase>>
    {
        protected readonly ILogger<ThanksInlineActionHandler> Logger;
        protected readonly IGratitudeService GratitudeService;

        protected static string Basic;
        protected static string Mocking;
        protected static string Shouting;
        protected static string Leet;

        public ThanksInlineActionHandler(ILogger<ThanksInlineActionHandler> logger, IGratitudeService gratitudeService, IConfiguration configuration)
        {
            Logger = logger;
            GratitudeService = gratitudeService;

            Basic = configuration["Images:Basic"];
            Mocking = configuration["Images:Mocking"];
            Shouting = configuration["Images:Shouting"];
            Leet = configuration["Images:Leet"];
        }

        public async Task<IEnumerable<InlineQueryResultBase>> Handle(ThanksInlineAction request, CancellationToken cancellationToken)
        {
            var gratitude = await GratitudeService.GetForEveryFilter(request.Name, cancellationToken: cancellationToken);

            if (gratitude.Count < 4)
            {
                throw new Exception();
            }

            return new List<InlineQueryResultBase>
            {
                new InlineQueryResultArticle(nameof(Basic), "Basic gratitude", new InputTextMessageContent(gratitude[0]))
                {
                    Description = gratitude[0],
                    ThumbUrl = Basic
                },
                new InlineQueryResultArticle(nameof(Mocking), "Mocking gratitude", new InputTextMessageContent(gratitude[1]))
                {
                    Description = gratitude[1],
                    ThumbUrl = Mocking

                },
                new InlineQueryResultArticle(nameof(Shouting), "Shouting gratitude", new InputTextMessageContent(gratitude[2]))
                {
                    Description = gratitude[2],
                    ThumbUrl = Shouting

                },
                new InlineQueryResultArticle(nameof(Leet), "Leet gratitude", new InputTextMessageContent(gratitude[3]))
                {
                    Description = gratitude[3],
                    ThumbUrl = Leet

                },
            };
        }
    }
}