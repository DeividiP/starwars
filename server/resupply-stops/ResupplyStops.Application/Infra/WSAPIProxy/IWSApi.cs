using Refit;
using ResupplyStops.Application.Domain.Model;
using System.Threading.Tasks;

namespace ResupplyStops.Application.Infra.WSAPIProxy
{
    public interface IWSApi
    {
        [Get("/starships/?page={page}")]
        Task<PageResult<StarShip>> GetStarShipsAsync(int? page = null);
    }
}
