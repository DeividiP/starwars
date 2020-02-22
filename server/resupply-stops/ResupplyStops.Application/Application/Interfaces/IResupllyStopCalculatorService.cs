using ResupplyStops.Application.Application.ViewModel;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ResupplyStops.Application.Application.Interfaces
{
    public interface IResupllyStopCalculatorService
    {
        Task<IList<StarShipResupplyStops>> CalculateAsync(int distance);
    }
}
