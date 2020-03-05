using Microsoft.AspNetCore.Mvc;
using Telegram.Bot.Types;

namespace Thankies.Bot.Api.Controllers
{
    [Route("api/[controller]")]
    public class UpdateController : ControllerBase
    {
        [HttpPost]
        public IActionResult Post([FromBody]Update update)
        {
            return Ok();
        }
    }
}