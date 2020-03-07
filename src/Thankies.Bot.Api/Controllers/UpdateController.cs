using System;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Thankies.Bot.Api.Client;
using Thankies.Core.Domain;
using Thankies.Core.Domain.Command.ThanksCommandAction;
using Thankies.Core.Domain.Inline.ThanksInlineAction;

namespace Thankies.Bot.Api.Controllers
{
    [Route("api/[controller]")]
    public class UpdateController : ControllerBase
    {
        protected readonly ILogger<UpdateController> Logger;
        protected readonly IBotClient BotClient;
        protected readonly IMediator Mediator;

        public UpdateController(ILogger<UpdateController> logger, IBotClient botClient, IMediator mediator)
        {
            Logger = logger;
            BotClient = botClient;
            Mediator = mediator;
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody]Update update)
        {
            if (update.Type == UpdateType.InlineQuery)
            {
                try
                {
                    var results = await Mediator.Send(new ThanksInlineAction(update));

                    await BotClient.Client.AnswerInlineQueryAsync(update.InlineQuery.Id, results, 1, true);
                }
                catch (Exception e)
                {
                    Logger.LogError(e, "Error sending inline responses.");
                }
            }
            else if (update.Type == UpdateType.Message)
            {
                try
                {
                    var result = await Mediator.Send(new ThanksCommandAction(update));

                    await BotClient.Client.SendTextMessageAsync(update.Message.Chat, result, ParseMode.Markdown, replyToMessageId: update.Message.ReplyToMessage?.MessageId ?? default);
                }
                catch (Exception e)
                {
                    Logger.LogError(e, "Error sending message.");

                }
            }

            return Ok();
        }
    }
}