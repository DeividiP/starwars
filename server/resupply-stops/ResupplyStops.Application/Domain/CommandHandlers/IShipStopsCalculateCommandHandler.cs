using ResupplyStops.Application.Domain.Query;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ResupplyStops.Application.Domain.CommandHandlers
{
    public interface IShipStopsCalculateCommandHandler
    {
        Task<List<ShipStopsCalculateQuery>> Handle(int distance);
    }
}
