using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.Extensions.Logging;
using Thankies.Infrastructure.Contract.Service;

namespace Thankies.Core.Domain.Command.ThanksCommandAction
{
    public class ThanksCommandActionHandler : IRequestHandler<ThanksCommandAction, (int?, string)>
    {
        protected readonly ILogger<ThanksCommandActionHandler> Logger;
        protected readonly IGratitudeService GratitudeService;

        public ThanksCommandActionHandler(ILogger<ThanksCommandActionHandler> logger, IGratitudeService gratitudeService)
        {
            Logger = logger;
            GratitudeService = gratitudeService;
        }

        public async Task<(int?, string)> Handle(ThanksCommandAction request, CancellationToken cancellationToken)
        {
            string gratitude;

            if (!request.IsReply || string.IsNullOrWhiteSpace(request.UserToReply))
            {
                gratitude = await GratitudeService.Get(null, null, "basic", "eng", cancellationToken);
            }
            else
            {
                gratitude = await GratitudeService.Get(request.UserToReply, null, "basic", "eng", cancellationToken);
            }

            return (request.MessageToReplyId, gratitude);
        }
    }
}