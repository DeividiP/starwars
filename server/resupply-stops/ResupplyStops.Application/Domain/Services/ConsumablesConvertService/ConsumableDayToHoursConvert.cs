using System.Collections.Generic;

namespace ResupplyStops.Application.Domain.Services
{
    class ConsumableDayToHoursConvert : ConsumableToHoursConvertBase, IConsumableToHoursConvert
    {
        protected override int PeriodsQuantityDays => 1;
        protected override List<string> ValidPeriodsToConvert
                    => new List<string> { "day", "days" };
    }
}
