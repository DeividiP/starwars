using ResupplyStops.Application.Domain.Model;
using ResupplyStops.Application.Domain.Services;
using System.Collections.Generic;
using Xunit;

namespace ResupplyStops.Application.Test.Domain.Model
{
    public class StarShipTest
    {
        private readonly IConsumablesConvertService _consumablesConvertService;
        private readonly List<IConsumableToHoursConvert> toHourConverters;
        private IStarShip _subject;
        public StarShipTest()
        {
            toHourConverters = new List<IConsumableToHoursConvert>()
            {
                new ConsumableWeekToHoursConvert(),
                new ConsumableMonthToHoursConvert()
            };

            _consumablesConvertService = new ConsumablesConvertService(toHourConverters);
        }

        [Theory]
        [InlineData("Y-wing", 80, "1 week", 74)]
        [InlineData("Millennium Falcon", 75, "2 months", 9)]
        [InlineData("Rebel Transport", 20, "6 months", 11)]
        public void Calculate_Should_Return_Properly_Stops_When_Distance_Is_1000000(string shipName, int mgltPerHour, string consumables, int expectedStops)
        {
            int distance = 1000000;

            _subject = new StarShip(_consumablesConvertService)
            {
                Name = shipName,
                MGLT = mgltPerHour,
                Consumables = consumables
            };

            var result = _subject.CalculateStops(distance);

            Assert.Equal(expectedStops, result);
        } 
    }
}
