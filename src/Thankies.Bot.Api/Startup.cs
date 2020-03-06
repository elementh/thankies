using System;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Polly;
using Scrutor;
using Thankies.Bot.Api.Client;
using Thankies.Bot.Api.Hosted;
using Thankies.Core.Domain;
using Thankies.Infrastructure.Contract.Client;
using Thankies.Infrastructure.Contract.Service;
using Thankies.Infrastructure.Implementation.Client;
using Thankies.Infrastructure.Implementation.Service;

namespace Thankies.Bot.Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers().AddNewtonsoftJson();

            #region Hosted

            services.AddSingleton<IBotClient, BotClient>();
            services.AddHostedService<SetWebHookHosted>();

            #endregion

            #region Pipeline

            services.AddMediatR(typeof(ThanksInlineAction).Assembly);

            #endregion

            #region Infrastructure

            services.AddHttpClient<ITaaSClient, TaasClient>()
                .AddTransientHttpErrorPolicy(builder =>
                    builder.WaitAndRetryAsync(3, retryCount =>
                        TimeSpan.FromSeconds(Math.Pow(2, retryCount))));

            services.AddScoped<IGratitudeService, GratitudeService>();

            #endregion
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
        }
    }
}