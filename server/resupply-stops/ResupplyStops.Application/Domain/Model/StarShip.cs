using ResupplyStops.Application.Domain.Services;

namespace ResupplyStops.Application.Domain.Model
{
    public class StarShip : IStarShip
    {
        private readonly IConsumablesConvertService _consumablesConvertService;

        public StarShip(IConsumablesConvertService consumablesConvertService)
        {
            _consumablesConvertService = consumablesConvertService;
        }

        public string Name { get; set; }
        public int MGLT { get; set; }
        public string Consumables { get; set; }

        public int CalculateStops(int distance)
        {
            var mgltAutonomy = CalculateMGLTAutonomy();

            return distance / mgltAutonomy;
        }
        private int CalculateMGLTAutonomy()
        {
            var consumablesInHours = _consumablesConvertService.ConvertToHours(Consumables);

            return consumablesInHours * MGLT;
        }
    }
}
