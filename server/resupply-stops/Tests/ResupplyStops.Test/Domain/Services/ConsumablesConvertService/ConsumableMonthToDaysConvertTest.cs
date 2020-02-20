using ResupplyStops.Application.Domain.Services;
using Xunit;

namespace ResupplyStops.Test.Domain.Services.ConsumablesConvertService
{
    public class ConsumableMonthToDaysConvertTest
    {
        private readonly IConsumableToHoursConvert _subject;

        public ConsumableMonthToDaysConvertTest()
        {
            _subject = new ConsumableMonthToHoursConvert();
        }

        [Theory]
        [InlineData("1 month")]
        [InlineData("2 months")]
        public void CanConvert_Should_ReturnTrue_To_Properly_Periods(string consumables)
        {
            var result = _subject.CanConvert(consumables);

            Assert.True(result);
        }

        [Theory]
        [InlineData("1 month", 720)]
        [InlineData("2 months", 1440)]
        public void Convert_Should_Return_The_Properly_Quantity_of_Hours(string consumables, int expectedResult)
        {
            var result = _subject.Convert(consumables);

            Assert.Equal(expectedResult, result);
        }
    }
}
