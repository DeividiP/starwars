using System.Collections.Generic;

namespace ResupplyStops.Application.Domain.Services
{
    public class ConsumableWeekToHoursConvert : IConsumableToHoursConvert
    {
        private const int hoursByDay = 24;

        private readonly List<string> validPeriodsToConvert 
                    = new List<string> { "week" };

        public int PeriodsQuantityDays => 7;
        public int Quantity { get; private set; }
        public string Period { get; private set; }

        public bool CanConvert(string consumable)
        {
            var splitedConsumable = consumable.Split(' ');

            var quantity = splitedConsumable[0];
            var period = splitedConsumable[1];

            if (!validPeriodsToConvert.Contains(period))
            {
                return false;
            }

            Quantity = System.Convert.ToInt32(quantity);
            Period = period;

            return true;
        }

        public int Convert(string consumable)
        {
            return PeriodsQuantityDays * Quantity * hoursByDay;
        }
    }
}
