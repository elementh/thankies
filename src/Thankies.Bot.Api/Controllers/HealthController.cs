using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Thankies.Bot.Api.VIewModel;

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
            var data = HealthConfigViewModel.ParseFromConfiguration(Configuration);

            return Ok(data);
        }
    }
}