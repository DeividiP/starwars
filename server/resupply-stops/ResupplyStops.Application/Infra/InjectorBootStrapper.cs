using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using Refit;
using ResupplyStops.Application.Application.Interfaces;
using ResupplyStops.Application.Application.Services;
using ResupplyStops.Application.Domain.CommandHandlers;
using ResupplyStops.Application.Domain.Interfaces;
using ResupplyStops.Application.Domain.Services;
using ResupplyStops.Application.Infra.WSAPIProxy;
using System.Collections.Generic;
using System.Linq;
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
            services.AddScoped<IConsumablesConvertService, ConsumablesConvertService>();
            services.AddScoped<IConsumableToHoursConvert, ConsumableMonthToHoursConvert>();
            services.AddScoped<IConsumableToHoursConvert, ConsumableWeekToHoursConvert>();
            services.AddScoped<IConsumableToHoursConvert, ConsumableYearToHoursConvert>();
            services.AddTransient<IList<IConsumableToHoursConvert>>(p => p.GetServices<IConsumableToHoursConvert>().ToList());

            //Infra
            services.AddScoped<IWSAPIProxy, InfraWSAPIProxy.WSAPIProxy>();
            services.AddScoped(factory => {

                //factory.GetService<ICo>

                var wsapiClient = RestService.For<IWSApi>("https://swapi.co/api", new RefitSettings
                {
                    ContentSerializer = new JsonContentSerializer(
                        new JsonSerializerSettings
                        {
                            ContractResolver = new CamelCasePropertyNamesContractResolver(),
                            ReferenceLoopHandling = ReferenceLoopHandling.Ignore
                        })
                });

                return wsapiClient;
            });
        }
    }
}
