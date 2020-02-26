using ResupplyStops.Application.Domain.Interfaces;
using ResupplyStops.Application.Domain.Model;
using System.Threading.Tasks;

namespace ResupplyStops.Application.Domain.Services
{
    public class ResupplyStopsCalculatorService : IResupplyStopsCalculatorService
    {
        private int mgltValue;
        private IConsumablesConvertService _consumablesConvertService;

        public ResupplyStopsCalculatorService(IConsumablesConvertService consumablesConvertService)
        {
            _consumablesConvertService = consumablesConvertService;
        }

        public async Task<int?> CalculateStopsAsync(StarShip starShip, int distance)
        {
            return await Task.FromResult(Calculate(starShip, distance));
        }

        private int? Calculate(StarShip starShip, int distance)
        {
            if (!int.TryParse(starShip.MGLT, out mgltValue))
                return null;

            var mgltAutonomy = CalculateMGLTAutonomy(starShip);

            return distance / mgltAutonomy;
        }
        private int CalculateMGLTAutonomy(StarShip starShip)
        {
            var consumablesInHours = _consumablesConvertService.ConvertToHours(starShip.Consumables);

            return consumablesInHours * mgltValue;
        }
    }
}
