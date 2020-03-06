using System.Collections.Generic;
using MediatR;
using Telegram.Bot.Types;
using Telegram.Bot.Types.InlineQueryResults;

namespace Thankies.Core.Domain
{
    public class ThanksInlineAction : IRequest<IEnumerable<InlineQueryResultBase>>
    {
        public ThanksInlineAction(Update update)
        {
            Name = string.IsNullOrWhiteSpace(update.InlineQuery.Query) ? null : update.InlineQuery.Query;
        }
        
        public string? Name { get; }
    }
}