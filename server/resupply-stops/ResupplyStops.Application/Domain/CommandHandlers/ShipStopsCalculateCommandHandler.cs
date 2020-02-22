﻿using ResupplyStops.Application.Domain.Interfaces;
using ResupplyStops.Application.Domain.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ResupplyStops.Application.Domain.CommandHandlers
{
    public class ShipStopsCalculateCommandHandler : IShipStopsCalculateCommandHandler
    {
        private readonly IWSAPIProxy _iWSAPIProxy;

        public ShipStopsCalculateCommandHandler(IWSAPIProxy iWSAPIProxy)
        {
            _iWSAPIProxy = iWSAPIProxy;
        }

        public async Task<List<ShipStopsCalculateQuery>> Handle(int distance)
        {
            ValidadeDistanceArgument(distance);

            var starships = _iWSAPIProxy.GetAllStarShips();

            var shipStopsResult = starships.Select(s => new ShipStopsCalculateQuery
            {
                Distance = distance,
                ShipName = s.Name,
                Stops = s.CalculateStops(distance),

            }).ToList();

            return await Task.FromResult(shipStopsResult);
        }

        private void ValidadeDistanceArgument(int distance)
        {
            if (distance < 0)
            {
                throw new ArgumentOutOfRangeException("distance","The distance must be greater than zero.");
            }
        }
    }
}
