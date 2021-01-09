using MediatR;
using Navigator.Abstraction;
using Navigator.Actions;
using Navigator.Actions.Abstraction;
using Telegram.Bot.Types;

namespace Thankies.Core.Domain.Command.ThanksCommandAction
{
    public class ThanksCommandAction : Action
    {
        public override string Type => ActionType.Command;
        public bool IsReply { get; protected set; }

        public override IAction Init(INavigatorContext ctx)
        {
            IsReply = ctx.Update.Message.ReplyToMessage?.MessageId != null;

            return this;
        }

        public override bool CanHandle(INavigatorContext ctx)
        {
            return ctx.Update.Message.Text.StartsWith("/thanks");
        }

    }
}