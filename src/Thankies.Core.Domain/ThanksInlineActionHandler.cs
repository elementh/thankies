using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Telegram.Bot.Types.InlineQueryResults;
using Thankies.Infrastructure.Contract.Service;

namespace Thankies.Core.Domain
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
            
            Basic = configuration["Images.Basic"];
            Mocking = configuration["Images.Mocking"];
            Shouting = configuration["Images.Shouting"];
            Leet = configuration["Images.Leet"];
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
                new InlineQueryResultPhoto(nameof(Basic), Basic, Basic)  {
                    Title = "Basic gratitude",
                    Description = gratitude[0],
                    InputMessageContent = new InputTextMessageContent(gratitude[0])
                },
                new InlineQueryResultPhoto(nameof(Basic), Basic, Basic)  {
                    Title = "Mocking gratitude",
                    Description = gratitude[0],
                    InputMessageContent = new InputTextMessageContent(gratitude[1])
                },
                new InlineQueryResultPhoto(nameof(Basic), Basic, Basic)  {
                    Title = "Shouting gratitude",
                    Description = gratitude[0],
                    InputMessageContent = new InputTextMessageContent(gratitude[2])
                },
                new InlineQueryResultPhoto(nameof(Basic), Basic, Basic)  {
                    Title = "Leet gratitude",
                    Description = gratitude[0],
                    InputMessageContent = new InputTextMessageContent(gratitude[3])
                }
            };
        }
    }
}