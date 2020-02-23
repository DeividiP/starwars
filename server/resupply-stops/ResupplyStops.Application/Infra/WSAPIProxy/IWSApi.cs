using Refit;
using System.Threading.Tasks;

namespace ResupplyStops.Application.Infra.WSAPIProxy
{
    public interface IWSApi
    {
        [Get("/starships/?page={page}")]
        Task<PageResult<StarShip>> GetStarShips(int? page);
    }
}
