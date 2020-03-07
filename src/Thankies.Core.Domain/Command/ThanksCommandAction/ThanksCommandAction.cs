using MediatR;
using Telegram.Bot.Types;

namespace Thankies.Core.Domain.Command.ThanksCommandAction
{
    public class ThanksCommandAction : IRequest<(int?, string)>
    {
        public readonly bool IsReply;
        public readonly string? UserToReply;
        public readonly int? MessageToReplyId;

        public ThanksCommandAction(Update update)
        {
            IsReply = update.Message.ReplyToMessage?.MessageId != null;

            if (!IsReply)
            {
                return;
            }

            if (string.IsNullOrWhiteSpace(update.Message.ReplyToMessage?.From?.Username))
            {
                UserToReply = update.Message.ReplyToMessage?.From?.Username;
            }
            else
            {
                MessageToReplyId = update.Message.ReplyToMessage.MessageId;
            }
        }
    }
}