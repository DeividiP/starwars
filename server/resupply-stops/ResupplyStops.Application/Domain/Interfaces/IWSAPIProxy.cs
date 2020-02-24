using ResupplyStops.Application.Domain.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ResupplyStops.Application.Domain.Interfaces
{
    public interface IWSAPIProxy
    {
        Task<List<StarShip>> GetAllStarShipsAsync();
    }
}
