using ResupplyStops.Application.Domain.Model;
using System.Collections.Generic;

namespace ResupplyStops.Application.Domain.Interfaces
{
    public interface IWSAPIProxy
    {
        List<IStarShip> GetAllStarShips();
    }
}
