using System;
using System.Net.Http;
using System.Text;
using System.Text.Json;
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
            client.BaseAddress = new Uri(configuration["TAAS_URL"]);
            Logger = logger;
            Client = client;
        }

        public async Task<GratitudeResponse> GetGratitude(string? name, string? filter, string language = "eng", CancellationToken cancellationToken = default)
        {
            var requestUri = $"Thanks?name={name ?? ""}&category=basic&filters={filter ?? ""}&language={language}";
            
            var requestResponse = await Client.GetAsync($"{Client.BaseAddress}{requestUri}", cancellationToken);

            requestResponse.EnsureSuccessStatusCode();
            
            await using var responseStream = await requestResponse.Content.ReadAsStreamAsync();
            
            var gratitude = await JsonSerializer.DeserializeAsync<GratitudeResponse>(responseStream, cancellationToken: cancellationToken);

            return gratitude;
        }
    }
}