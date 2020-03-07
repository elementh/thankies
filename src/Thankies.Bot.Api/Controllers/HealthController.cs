using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Thankies.Bot.Api.ViewModel;
using Thankies.Infrastructure.Contract.Service;

namespace Thankies.Bot.Api.Controllers
{
    [Route("api/[controller]")]
    public class HealthController : ControllerBase
    {
        public readonly IConfiguration Configuration;
        public readonly IGratitudeService GratitudeService;

        public HealthController(IConfiguration configuration, IGratitudeService gratitudeService)
        {
            Configuration = configuration;
            GratitudeService = gratitudeService;
        }
        
        [HttpGet, Route("config")]
        [Produces("application/json")]
        public IActionResult GetConfigHealth(CancellationToken cancellationToken)
        {
            var data = HealthConfigViewModel.ParseFromConfiguration(Configuration);

            return Ok(data);
        }
        
        [HttpGet, Route("infrastructure")]
        [Produces("application/json")]
        public async Task<IActionResult> GetInfrastructureHealth(CancellationToken cancellationToken)
        {
            var gratitude = await GratitudeService.Get("Thankies", null, "eng", cancellationToken);

            var gratitudeFilters = await GratitudeService.GetForEveryFilter("Thankies", "eng", cancellationToken);
            
            return Ok(new HealthInfrastructureViewModel(gratitude, gratitudeFilters));
        }
    }
}