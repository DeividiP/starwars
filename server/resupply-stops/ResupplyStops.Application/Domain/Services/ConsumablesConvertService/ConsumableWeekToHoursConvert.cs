using System.Collections.Generic;

namespace ResupplyStops.Application.Domain.Services
{
    public class ConsumableWeekToHoursConvert : ConsumableToHoursConvertBase, IConsumableToHoursConvert
    {
        public override int PeriodsQuantityDays => 7;
        protected override List<string> ValidPeriodsToConvert 
                    => new List<string> { "week", "weeks" };
    }
}
