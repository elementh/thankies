using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Thankies.Bot.Api.Client;

namespace Thankies.Bot.Api.Hosted
{
    public class SetWebHookBackgroundService : BackgroundService
    {
        private readonly IServiceScopeFactory _serviceScopeFactory;
        private readonly ILogger<SetWebHookBackgroundService> _logger;

        public SetWebHookBackgroundService(IServiceScopeFactory serviceScopeFactory, ILogger<SetWebHookBackgroundService> logger)
        {
            _serviceScopeFactory = serviceScopeFactory;
            _logger = logger;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            using var scope = _serviceScopeFactory.CreateScope();
            
            var botClient = scope.ServiceProvider.GetRequiredService<IBotClient>();

            await botClient.Client.SetWebhookAsync(botClient.BotUrl, cancellationToken: stoppingToken);

            var me = await botClient.Client.GetMeAsync(stoppingToken);
                
            _logger.LogInformation($"Telegram Bot Client is receiving updates for bot: @{me.Username} at the url: {botClient.BotUrl}");
        }
    }
}