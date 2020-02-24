using ResupplyStops.Application.Domain.Services;

namespace ResupplyStops.Application.Domain.Model
{
    public class StarShip : IStarShip
    {
        private int mgltValue;
        private IConsumablesConvertService _consumablesConvertService;

        public string Name { get; set; }
        public string MGLT { get; set; }
        public string Consumables { get; set; }

        public virtual int? CalculateStops(int distance, IConsumablesConvertService consumablesConvertService)
        {
            if (!int.TryParse(MGLT, out mgltValue))
                return null;

            _consumablesConvertService = consumablesConvertService;

            var mgltAutonomy = CalculateMGLTAutonomy();

            return distance / mgltAutonomy;
        }
        private int CalculateMGLTAutonomy()
        {
            var consumablesInHours = _consumablesConvertService.ConvertToHours(Consumables);

            return consumablesInHours * mgltValue;
        }
    }
}
