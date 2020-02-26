using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using Polly;
using Polly.Extensions.Http;
using Refit;
using ResupplyStops.Application.Application.Interfaces;
using ResupplyStops.Application.Application.Services;
using ResupplyStops.Application.Domain.CommandHandlers;
using ResupplyStops.Application.Domain.Interfaces;
using ResupplyStops.Application.Domain.Services;
using ResupplyStops.Application.Infra.WSAPIProxy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using InfraWSAPIProxy = ResupplyStops.Application.Infra.WSAPIProxy;

namespace ResupplyStops.Application.Infra
{
    public class InjectorBootStrapper
    {
        public static void RegisterServices(IServiceCollection services)
        {
            // Application Services
            services.AddScoped<IResupllyStopCalculatorService, ResupllyStopCalculatorService>();
            //Domain Command Handlers
            services.AddScoped<IStarShipResupplyStopsCalculateCommandHandler, StarShipResupplyStopsCalculateCommandHandler>();
            //Domain Services
            services.AddScoped<IResupplyStopsCalculatorService, ResupplyStopsCalculatorService>();
            services.AddScoped<IConsumablesConvertService, ConsumablesConvertService>();
            services.AddScoped<IConsumableToHoursConvert, ConsumableMonthToHoursConvert>();
            services.AddScoped<IConsumableToHoursConvert, ConsumableWeekToHoursConvert>();
            services.AddScoped<IConsumableToHoursConvert, ConsumableYearToHoursConvert>();
            services.AddScoped<IConsumableToHoursConvert, ConsumableDayToHoursConvert>();
            services.AddTransient<IList<IConsumableToHoursConvert>>(p => p.GetServices<IConsumableToHoursConvert>().ToList());
            //Infra
            services.AddScoped<IWSAPIProxy, InfraWSAPIProxy.WSAPIProxy>();
            services.AddHttpClient("wsApiClient")
              .ConfigureHttpClient((serviceProvider, conf) =>
              {
                  var wsApiOptionValue = serviceProvider.GetService<IOptions<WSApiSettings>>().Value;
                  conf.BaseAddress = new Uri(wsApiOptionValue.BaseUrl);
              })
              .AddPolicyHandler(HttpPolicyExtensions
              .HandleTransientHttpError()
              .WaitAndRetryAsync(3, retryAttempt => TimeSpan.FromSeconds(Math.Pow(2, retryAttempt))));
            services.AddScoped(serviceProvider => 
            {
                var httpFactory = serviceProvider.GetService<IHttpClientFactory>();
                var wshttpApiClient = httpFactory.CreateClient("wsApiClient");

                var wsApiClient = RestService.For<IWSApi>(wshttpApiClient, new RefitSettings
                {
                    ContentSerializer = new JsonContentSerializer(
                        new JsonSerializerSettings
                        {
                            ContractResolver = new CamelCasePropertyNamesContractResolver(),
                        }),
                });
                return wsApiClient;
            });
        }
    }
}
