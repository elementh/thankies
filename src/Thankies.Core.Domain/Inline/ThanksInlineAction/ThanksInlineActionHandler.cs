using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Navigator.Abstraction;
using Navigator.Actions;
using Telegram.Bot.Types.InlineQueryResults;
using Thankies.Infrastructure.Contract.Service;

namespace Thankies.Core.Domain.Inline.ThanksInlineAction
{
    public class ThanksInlineActionHandler : ActionHandler<ThanksInlineAction>
    {
        protected readonly IGratitudeService GratitudeService;

        protected static string Basic;
        protected static string Mocking;
        protected static string Shouting;
        protected static string Leet;
        protected static string ArtCute;

        public ThanksInlineActionHandler(INavigatorContext ctx, IConfiguration configuration, IGratitudeService gratitudeService) : base(ctx)
        {
            GratitudeService = gratitudeService;
            Basic = configuration["Images:Basic"];
            Mocking = configuration["Images:Mocking"];
            Shouting = configuration["Images:Shouting"];
            Leet = configuration["Images:Leet"];
            ArtCute = configuration["Images:ArtCute"];
        }
        
        public override async Task<Unit> Handle(ThanksInlineAction request, CancellationToken cancellationToken)
        {
            var gratitude = await GratitudeService.GetForEveryFilter(request.Name, cancellationToken: cancellationToken);
            var gratitudeArt = await GratitudeService.Get(request.Name, null, "art", cancellationToken: cancellationToken);

            if (gratitude.Count < 4 || string.IsNullOrWhiteSpace(gratitudeArt))
            {
                return Unit.Value;
            }

            var responses = new List<InlineQueryResultBase>
            {
                new InlineQueryResultArticle(nameof(Basic), "Basic gratitude", new InputTextMessageContent(gratitude[0]))
                {
                    Description = gratitude[0],
                    ThumbUrl = Basic
                },
                new InlineQueryResultArticle(nameof(Mocking), "Mocking gratitude", new InputTextMessageContent(gratitude[1]))
                {
                    Description = gratitude[2],
                    ThumbUrl = Mocking

                },
                new InlineQueryResultArticle(nameof(Shouting), "Shouting gratitude", new InputTextMessageContent(gratitude[2]))
                {
                    Description = gratitude[3],
                    ThumbUrl = Shouting

                },
                new InlineQueryResultArticle(nameof(Leet), "Leet gratitude", new InputTextMessageContent(gratitude[3]))
                {
                    Description = gratitude[1],
                    ThumbUrl = Leet
                },
                new InlineQueryResultArticle(nameof(ArtCute), "Art/Cute gratitude", new InputTextMessageContent(gratitudeArt))
                {
                    Description = gratitudeArt,
                    ThumbUrl = ArtCute
                }
            };
            
            await Ctx.Client.AnswerInlineQueryAsync(Ctx.Update.InlineQuery.Id, responses, 1, true, cancellationToken: cancellationToken);
            
            return Unit.Value;
        }
    }
}