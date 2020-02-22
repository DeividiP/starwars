using AutoMapper;
using ResupplyStops.Application.Application.ViewModel;
using ResupplyStops.Application.Domain.Query;
using System.Collections.Generic;

namespace ResupplyStops.Application.Application.AutoMapperProfiles
{
    public class DomainToAplicationViewModelMappingProfile : Profile
    {
        public DomainToAplicationViewModelMappingProfile()
        {
            CreateMap<ShipStopsCalculateQuery, StarShipResupplyStops>();
            CreateMap<List<ShipStopsCalculateQuery>, List<StarShipResupplyStops>>();
        }
    }
}
