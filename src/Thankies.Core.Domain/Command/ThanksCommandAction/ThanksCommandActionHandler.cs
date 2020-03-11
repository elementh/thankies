using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.Extensions.Logging;
using Navigator.Abstraction;
using Navigator.Actions;
using Thankies.Infrastructure.Contract.Service;

namespace Thankies.Core.Domain.Command.ThanksCommandAction
{
    public class ThanksCommandActionHandler : ActionHandler<ThanksCommandAction>
    {
        protected readonly IGratitudeService GratitudeService;
        
        public ThanksCommandActionHandler(INavigatorContext ctx, IGratitudeService gratitudeService) : base(ctx)
        {
            GratitudeService = gratitudeService;
        }
        
        public override async Task<Unit> Handle(ThanksCommandAction request, CancellationToken cancellationToken)
        {
            var gratitude = await GratitudeService.Get(null, null, "basic", "eng", cancellationToken);
            if (gratitude == null) return Unit.Value;
            
            if (request.IsReply)
            {
                await Ctx.Client.SendTextMessageAsync(Ctx.Update.Message.Chat.Id, gratitude,
                    replyToMessageId: Ctx.Update.Message.ReplyToMessage.MessageId, cancellationToken: cancellationToken);
            }
            else
            {     
                await Ctx.Client.SendTextMessageAsync(Ctx.Update.Message.Chat.Id, gratitude, cancellationToken: cancellationToken);
            }

            return Unit.Value;
        }

    }
}