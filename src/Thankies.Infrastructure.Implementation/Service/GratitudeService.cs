using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Thankies.Infrastructure.Contract.Client;
using Thankies.Infrastructure.Contract.Model;
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

        public async Task<string?> Get(string? name = null, string? filter = null, string category = "basic", string language = "eng", CancellationToken cancellationToken = default)
        {
            try
            {
                var gratitude = await Client.GetGratitude(name, filter, category, language, cancellationToken);

                return gratitude.Text;
            }
            catch (Exception e)
            {
                Logger.LogError(e, "Error while fetching gratitude from taas.");

                return null;
            }
        }

        public async Task<List<string>> GetForEveryFilter(string? name = null, string language = "eng", CancellationToken cancellationToken = default)
        {
            var gratitude = new List<string>();
            
            try
            {
                var gratitudeResponse = await Client.GetGratitudeAllFilters(name, language, cancellationToken);

                gratitude.Add(gratitudeResponse.Text);
                
                gratitude.AddRange(gratitudeResponse.Flavours.Select(x => x.Text).Skip(1));

                return gratitude;
            }
            catch (Exception e)
            {
                Logger.LogError(e, "Error while fetching gratitude from taas.");

                return gratitude;
            }
        }
    }
}