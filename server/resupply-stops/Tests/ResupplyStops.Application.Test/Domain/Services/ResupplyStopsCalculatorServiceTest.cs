using ResupplyStops.Application.Domain.Interfaces;
using ResupplyStops.Application.Domain.Model;
using ResupplyStops.Application.Domain.Services;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace ResupplyStops.Application.Test.Domain.Services
{
    public class ResupplyStopsCalculatorServiceTest
    {
        private readonly IConsumablesConvertService _consumablesConvertService;
        private readonly List<IConsumableToHoursConvert> toHourConverters;
        private IResupplyStopsCalculatorService _subject;
        public ResupplyStopsCalculatorServiceTest()
        {
            toHourConverters = new List<IConsumableToHoursConvert>()
            {
                new ConsumableWeekToHoursConvert(),
                new ConsumableMonthToHoursConvert()
            };

            _consumablesConvertService = new ConsumablesConvertService(toHourConverters);

            _subject = new ResupplyStopsCalculatorService(_consumablesConvertService);
        }

        [Theory]
        [InlineData("Y-wing", "80", "1 week", 74)]
        [InlineData("Millennium Falcon", "75", "2 months", 9)]
        [InlineData("Rebel Transport", "20", "6 months", 11)]
        public async Task Calculate_Should_Return_Properly_Stops_When_Distance_Is_1000000(string shipName, string mgltPerHour, string consumables, int expectedStops)
        {
            int distance = 1000000;

            var starShip = new StarShip()
            {
                Name = shipName,
                MGLT = mgltPerHour,
                Consumables = consumables
            };

            var result = await _subject.CalculateStopsAsync(starShip, distance);

            Assert.Equal(expectedStops, result);
        }

        [Fact]
        public async Task Calculate_Should_Return_NULL_When_MGLT_Is_Unknown()
        {
            int distance = 1000000;

            var starShip = new StarShip()
            {
                Name = "ship name",
                MGLT = "unknown",
                Consumables = "1 Period"
            };

            var result = await _subject.CalculateStopsAsync(starShip, distance);

            Assert.Null(result);
        }
    }
}
