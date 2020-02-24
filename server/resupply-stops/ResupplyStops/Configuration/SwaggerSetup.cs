using Microsoft.Extensions.DependencyInjection;
using System;
using Microsoft.OpenApi.Models;
using System.IO;

namespace ResupplyStops.Configuration
{
    public static class SwaggerSetup
    {
        public static void AddSwaggerSetup(this IServiceCollection services)
        {
            if (services == null) throw new ArgumentNullException(nameof(services));

            services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new OpenApiInfo{ Title = "SW Challenge", Version = "v1" });
                options.DocInclusionPredicate((docName, description) => true);
                options.CustomSchemaIds(x => x.FullName);
                var configFilePath = Path.Combine(AppContext.BaseDirectory, "ResupplyStops.xml");
                options.IncludeXmlComments(configFilePath);
            });
        }
    }
}
