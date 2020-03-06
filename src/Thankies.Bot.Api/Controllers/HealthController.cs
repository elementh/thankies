using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace Thankies.Bot.Api.Controllers
{
    [Route("api/[controller]")]
    public class HealthController : ControllerBase
    {
        public readonly IConfiguration Configuration;

        public HealthController(IConfiguration configuration)
        {
            Configuration = configuration;
        }
        
        [HttpGet, Route("config")]
        [Produces("application/json")]
        public IActionResult GetConfigHealth()
        {
            var data = new
            {
                Basic = Configuration["Images:Basic"],
                Mocking = Configuration["Images:Mocking"],
                Shouting = Configuration["Images:Shouting"],
                Leet = Configuration["Images:Leet"]
            };

            return Ok(data);
        }
    }
}