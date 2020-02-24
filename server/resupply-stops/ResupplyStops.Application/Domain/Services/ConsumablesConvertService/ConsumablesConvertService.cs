using System.Collections.Generic;
using System;

namespace ResupplyStops.Application.Domain.Services
{
    public class ConsumablesConvertService : IConsumablesConvertService
    {
        private readonly IList<IConsumableToHoursConvert> toHourConverters;

        public ConsumablesConvertService(IList<IConsumableToHoursConvert> _toHourConverters)
        {
            toHourConverters = _toHourConverters;
        }

        public int ConvertToHours(string consumables)
        {
            foreach (var converter in toHourConverters)
            {
                if (converter.CanConvert(consumables))
                    return converter.Convert(consumables);
            }

            throw new InvalidOperationException($"Consumable convert was not found to: {consumables}");
        }
    }
}
