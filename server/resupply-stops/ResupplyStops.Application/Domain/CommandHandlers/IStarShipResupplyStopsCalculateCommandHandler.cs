using ResupplyStops.Application.Domain.Command;
using ResupplyStops.Application.Domain.Query;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ResupplyStops.Application.Domain.CommandHandlers
{
    public interface IStarShipResupplyStopsCalculateCommandHandler
    {
        Task<List<ShipStopsCalculateQuery>> Handle(StarShipResupplyStopsCalculateCommand command);
    }
}
