using AutoMapper;
using Microsoft.Extensions.DependencyInjection;
using ResupplyStops.Application.Application.AutoMapperProfiles;
using System;

namespace ResupplyStops.Application.Infra
{
    public static class AutoMapperSetup
    {
        public static void AddAutoMapperSetup(this IServiceCollection services)
        {
            if (services == null) throw new ArgumentNullException(nameof(services));

            services.AddAutoMapper(typeof(DomainToAplicationViewModelMappingProfile), typeof(DomainToAplicationViewModelMappingProfile));
        }
    }
}
