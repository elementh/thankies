using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Thankies.Infrastructure.Contract.Client;
using Thankies.Infrastructure.Contract.Model;

namespace Thankies.Infrastructure.Implementation.Client
{
    public class TaasClient : ITaaSClient
    {
        protected readonly ILogger<TaasClient> Logger;
        protected readonly HttpClient Client;

        public TaasClient(ILogger<TaasClient> logger, HttpClient client, IConfiguration configuration)
        {
            if (string.IsNullOrWhiteSpace(configuration["TAAS_URL"])) throw new ArgumentNullException(nameof(configuration), "Taas URL must not be null");
            
            client.BaseAddress = new Uri(configuration["TAAS_URL"]);
            Logger = logger;
            Client = client;
        }

        public async Task<GratitudeResponse> GetGratitude(string? name = null, string? filter = null, string category = "basic", string language = "eng", CancellationToken cancellationToken = default)
        {
            var requestUri = $"Thanks?name={name ?? ""}&category={category}&filters={filter ?? ""}&language={language}";
            
            var requestResponse = await Client.GetAsync($"{Client.BaseAddress}{requestUri}", cancellationToken);

            requestResponse.EnsureSuccessStatusCode();
            
            await using var responseStream = await requestResponse.Content.ReadAsStreamAsync();
            
            var gratitude = await JsonSerializer.DeserializeAsync<GratitudeResponse>(responseStream, cancellationToken: cancellationToken);

            return gratitude;
        }

        public async Task<IEnumerable<GratitudeResponse>> GetGratitudeAllFilters(string? name = null, string language = "eng", CancellationToken cancellationToken = default)
        {
            var requestUri = $"Bulk/allfilters?name={name ?? ""}&category=basic&different=true&language={language}";
            
            var requestResponse = await Client.GetAsync($"{Client.BaseAddress}{requestUri}", cancellationToken);
            
            requestResponse.EnsureSuccessStatusCode();
            
            await using var responseStream = await requestResponse.Content.ReadAsStreamAsync();
            
            var gratitude = await JsonSerializer.DeserializeAsync<IEnumerable<GratitudeResponse>>(responseStream, cancellationToken: cancellationToken);

            return gratitude;
        }
    }
}