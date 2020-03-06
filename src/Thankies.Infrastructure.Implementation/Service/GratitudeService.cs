using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Thankies.Infrastructure.Contract.Client;
using Thankies.Infrastructure.Contract.Service;

namespace Thankies.Infrastructure.Implementation.Service
{
    public class GratitudeService : IGratitudeService
    {
        protected readonly ILogger<GratitudeService> Logger;
        protected readonly ITaaSClient Client;

        public GratitudeService(ILogger<GratitudeService> logger, ITaaSClient client)
        {
            Logger = logger;
            Client = client;
        }

        public async Task<string?> Get(string? name, string? filter, string language = "eng", CancellationToken cancellationToken = default)
        {
            try
            {
                var gratitude = await Client.GetGratitude(name, filter, language, cancellationToken);

                return gratitude.Text;
            }
            catch (Exception e)
            {
                Logger.LogError(e, "Error while fetching gratitude from taas.");

                return null;
            }
        }
    }
}