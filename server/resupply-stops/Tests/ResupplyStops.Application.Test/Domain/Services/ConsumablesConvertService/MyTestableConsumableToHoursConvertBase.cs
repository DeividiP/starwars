using ResupplyStops.Application.Domain.Services;
using System.Collections.Generic;

namespace ResupplyStops.Application.Test.Domain.Services
{
    public class MyTestableConsumableToHoursConvertBase : ConsumableToHoursConvertBase
    {
        private List<string> _validPeriodsToConvert;
        protected override List<string> ValidPeriodsToConvert => _validPeriodsToConvert;

        private int _periodsQuantityDays;
        protected override int PeriodsQuantityDays => _periodsQuantityDays;

        public void SetInternalParametrs(int periodsQuantityDays, List<string> validPeriodsToConvert)
        {
            _periodsQuantityDays = periodsQuantityDays;
            _validPeriodsToConvert = validPeriodsToConvert;
        }
    }
}
