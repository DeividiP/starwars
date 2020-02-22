using System;
using System.Collections.Generic;

namespace ResupplyStops.Application.Domain.Services
{
    public abstract class ConsumableToHoursConvertBase : IConsumableToHoursConvert
    {
        private const int hoursByDay = 24;
        protected abstract List<string> ValidPeriodsToConvert { get; }
        protected abstract int PeriodsQuantityDays{ get; }
        public int Quantity { get; private set; }
        public string Period { get; private set; }

        public bool CanConvert(string consumable)
        {
            ParseConsumable(consumable);

            if (!ValidPeriodsToConvert.Contains(Period))
            {
                return false;
            }

            return true;
        }

        public int Convert(string consumable)
        {
            ParseConsumable(consumable);
            return PeriodsQuantityDays * Quantity * hoursByDay;
        }

        private void ParseConsumable(string consumable)
        {
            try
            {
                var splittedConsumable = consumable.Split(' ');
                Quantity = System.Convert.ToInt32(splittedConsumable[0]);
                Period = splittedConsumable[1];
            }
            catch (Exception)
            {
                throw new ArgumentException($"Consumable was unable to parse, converter: {this.GetType().Name}, cosumable value: {consumable}");
            }
        }
    }
}
