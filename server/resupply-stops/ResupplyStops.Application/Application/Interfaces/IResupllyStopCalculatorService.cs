using ResupplyStops.Application.Application.ViewModel;
using System.Threading.Tasks;

namespace ResupplyStops.Application.Application.Interfaces
{
    public interface IResupllyStopCalculatorService
    {
        Task<StarShipResupplyStopsList> CalculateAsync(int distance);
    }
}
