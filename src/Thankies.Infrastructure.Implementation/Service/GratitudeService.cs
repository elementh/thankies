using System;
using System.Collections.Generic;
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

        public async Task<List<(string, string)>> GetForEveryFilter(string? name, string language = "eng",
            CancellationToken cancellationToken = default)
        {
            try
            {
                var gratitude = new List<(string, string)>
                {
                    ("basic", await Get(name, GratitudeFilter.None, language, cancellationToken)),
                    (GratitudeFilter.Mocking, await Get(name, GratitudeFilter.Mocking, language, cancellationToken)),
                    (GratitudeFilter.Shouting, await Get(name, GratitudeFilter.Shouting, language, cancellationToken)),
                    (GratitudeFilter.Leet, await Get(name, GratitudeFilter.Leet, language, cancellationToken))
                };
                
                return gratitude;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }
    }
}