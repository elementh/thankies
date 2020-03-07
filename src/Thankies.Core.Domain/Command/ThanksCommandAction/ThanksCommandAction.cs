using MediatR;
using Telegram.Bot.Types;

namespace Thankies.Core.Domain.Command.ThanksCommandAction
{
    public class ThanksCommandAction : IRequest<string>
    {
        public ThanksCommandAction(Update update)
        {
            
        }
    }
}