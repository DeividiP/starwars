using ResupplyStops.Application.Domain.Services;

namespace ResupplyStops.Application.Domain.Model
{
    public class StarShip
    {
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
            var consumablesInHours = ConvertConsumablesPeriodToHours();

            return consumablesInHours * MGLT;
        }

        private int ConvertConsumablesPeriodToHours()
        {
            var weekConverter = new ConsumableWeekToHoursConvert();

            if (weekConverter.CanConvert(Consumables))
                return weekConverter.Convert(Consumables);

            var monthConverter = new ConsumableMonthToHoursConvert();

            if (monthConverter.CanConvert(Consumables))
                return monthConverter.Convert(Consumables);

            return 0;
        }
    }
}
