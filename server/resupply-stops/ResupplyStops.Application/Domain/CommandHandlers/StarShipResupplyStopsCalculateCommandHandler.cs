using Microsoft.Extensions.Logging;
using ResupplyStops.Application.Domain.Command;
using ResupplyStops.Application.Domain.Interfaces;
using ResupplyStops.Application.Domain.Query;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ResupplyStops.Application.Domain.CommandHandlers
{
    public class StarShipResupplyStopsCalculateCommandHandler : IStarShipResupplyStopsCalculateCommandHandler
    {
        private readonly IWSAPIProxy _iWSAPIProxy;
        private readonly IResupplyStopsCalculatorService _resupplyStopsCalculatorService;

        public StarShipResupplyStopsCalculateCommandHandler(
            IWSAPIProxy iWSAPIProxy,
            IResupplyStopsCalculatorService resupplyStopsCalculatorService)
        {
            _iWSAPIProxy = iWSAPIProxy;
            _resupplyStopsCalculatorService = resupplyStopsCalculatorService;
        }

        public async Task<List<ShipStopsCalculateQuery>> HandleAsync(StarShipResupplyStopsCalculateCommand command)
        {
            command.Validate();

            var starships = await _iWSAPIProxy.GetAllStarShipsAsync();

            var shipStopsResult = starships
                .Select(s => new ShipStopsCalculateQuery
                {
                    Distance = command.Distance,
                    Name = s.Name,
                    Stops = _resupplyStopsCalculatorService.CalculateStopsAsync(s, command.Distance).Result,
                }).ToList();

            return shipStopsResult;
        }
    }
}
