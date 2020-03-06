using System.Collections.Generic;
using MediatR;
using Telegram.Bot.Types.InlineQueryResults;

namespace Thankies.Core.Domain
{
    public class ThanksInlineAction : IRequest<IEnumerable<InlineQueryResultBase>>
    {
        
    }
}