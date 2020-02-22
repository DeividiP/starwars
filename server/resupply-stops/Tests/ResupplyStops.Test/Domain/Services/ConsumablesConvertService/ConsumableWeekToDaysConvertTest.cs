using ResupplyStops.Application.Domain.Services;
using Xunit;

namespace ResupplyStops.Test.Domain.Services
{
    public class ConsumableWeekToDaysConvertTest
    {
        private readonly IConsumableToHoursConvert _subject;

        public ConsumableWeekToDaysConvertTest()
        {
            _subject = new ConsumableWeekToHoursConvert();
        }

        [Theory]
        [InlineData("1 week")]
        [InlineData("2 weeks")]
        public void CanConvert_Should_ReturnTrue_To_Properly_Periods(string consumables)
        {
            var result = _subject.CanConvert(consumables);

            Assert.True(result);
        }

        [Theory]
        [InlineData("1 week", 168)]
        [InlineData("1 weeks", 168)]
        public void Convert_Should_Return_The_Properly_Quantity_of_Hours(string consumables, int expectedResult)
        {
            var result = _subject.Convert(consumables);

            Assert.Equal(expectedResult, result);
        }
    }
}
