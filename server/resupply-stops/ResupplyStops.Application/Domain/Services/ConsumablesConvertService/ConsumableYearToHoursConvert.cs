using System.Collections.Generic;

namespace ResupplyStops.Application.Domain.Services
{
    public class ConsumableYearToHoursConvert : ConsumableToHoursConvertBase, IConsumableToHoursConvert
    {
        protected override int PeriodsQuantityDays => 365;
        protected override List<string> ValidPeriodsToConvert
                    => new List<string> { "year", "years" };
    }
}
