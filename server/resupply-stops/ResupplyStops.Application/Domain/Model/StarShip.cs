using ResupplyStops.Application.Domain.Services;

namespace ResupplyStops.Application.Domain.Model
{
    public class StarShip : IStarShip
    {
        private int mgltValue;
        private readonly IConsumablesConvertService _consumablesConvertService;
        public StarShip() { }
        public StarShip(IConsumablesConvertService consumablesConvertService)
        {
            _consumablesConvertService = consumablesConvertService;
        }

        public string Name { get; set; }
        public string MGLT { get; set; }
        public string Consumables { get; set; }

        public virtual int? CalculateStops(int distance)
        {
            if (!int.TryParse(MGLT, out mgltValue))
                return null;

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
