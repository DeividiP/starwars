using ResupplyStops.Application.Domain.Command;
using ResupplyStops.Application.Domain.Interfaces;
using ResupplyStops.Application.Domain.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ResupplyStops.Application.Domain.CommandHandlers
{
    public class StarShipResupplyStopsCalculateCommandHandler : IStarShipResupplyStopsCalculateCommandHandler
    {
        private readonly IWSAPIProxy _iWSAPIProxy;

        public StarShipResupplyStopsCalculateCommandHandler(IWSAPIProxy iWSAPIProxy)
        {
            _iWSAPIProxy = iWSAPIProxy;
        }

        public async Task<List<ShipStopsCalculateQuery>> HandleAsync(StarShipResupplyStopsCalculateCommand command)
        {
            command.Validate();

            var starships = await _iWSAPIProxy.GetAllStarShipsAsync();

            var shipStopsResult = starships.Select(s => new ShipStopsCalculateQuery
            {
                Distance = command.Distance,
                Name = s.Name,
                Stops = s.CalculateStops(command.Distance),

            }).ToList();

            return await Task.FromResult(shipStopsResult);
        }
    }
}
