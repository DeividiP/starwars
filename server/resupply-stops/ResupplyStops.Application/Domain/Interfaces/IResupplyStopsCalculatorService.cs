using ResupplyStops.Application.Domain.Model;
using System.Threading.Tasks;

namespace ResupplyStops.Application.Domain.Interfaces
{
    public interface IResupplyStopsCalculatorService
    {
        Task<int?> CalculateStopsAsync(StarShip starShip, int distance);
    }
}
