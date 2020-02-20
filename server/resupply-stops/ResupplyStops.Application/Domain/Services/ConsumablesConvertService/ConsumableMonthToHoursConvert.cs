using System.Collections.Generic;

namespace ResupplyStops.Application.Domain.Services
{
    public class ConsumableMonthToHoursConvert : ConsumableToHoursConvertBase, IConsumableToHoursConvert
    {
        protected override int PeriodsQuantityDays => 30;

        protected override List<string> ValidPeriodsToConvert
             => new List<string> { "month", "months" };
    }
}
