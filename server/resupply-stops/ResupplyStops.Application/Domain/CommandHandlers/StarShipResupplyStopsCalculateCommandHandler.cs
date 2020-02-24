using ResupplyStops.Application.Domain.Command;
using ResupplyStops.Application.Domain.Interfaces;
using ResupplyStops.Application.Domain.Query;
using ResupplyStops.Application.Domain.Services;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ResupplyStops.Application.Domain.CommandHandlers
{
    public class StarShipResupplyStopsCalculateCommandHandler : IStarShipResupplyStopsCalculateCommandHandler
    {
        private readonly IWSAPIProxy _iWSAPIProxy;
        private readonly IConsumablesConvertService _consumablesConvertService;

        public StarShipResupplyStopsCalculateCommandHandler(
            IWSAPIProxy iWSAPIProxy,
            IConsumablesConvertService consumablesConvertService)
        {
            _iWSAPIProxy = iWSAPIProxy;
            _consumablesConvertService = consumablesConvertService;
        }

        public async Task<List<ShipStopsCalculateQuery>> HandleAsync(StarShipResupplyStopsCalculateCommand command)
        {
            command.Validate();

            var starships = await _iWSAPIProxy.GetAllStarShipsAsync();

            var shipStopsResult = starships.Select(s => new ShipStopsCalculateQuery
            {
                Distance = command.Distance,
                Name = s.Name,
                Stops = s.CalculateStops(command.Distance, _consumablesConvertService),

            }).ToList();

            return shipStopsResult;
        }
    }
}
