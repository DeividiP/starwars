using Microsoft.Extensions.DependencyInjection;
using ResupplyStops.Application.Infra;
using System;

namespace ResupplyStops.Configuration
{
    public static class DependencyInjectionSetup
    {
        public static void AddDependencyInjectionSetup(this IServiceCollection services)
        {
            if (services == null) throw new ArgumentNullException(nameof(services));

            InjectorBootStrapper.RegisterServices(services);
        }
    }
}
