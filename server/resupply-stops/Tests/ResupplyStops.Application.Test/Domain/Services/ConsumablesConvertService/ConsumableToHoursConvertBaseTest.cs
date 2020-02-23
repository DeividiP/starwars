using ResupplyStops.Application.Domain.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace ResupplyStops.Application.Test.Domain.Services
{
    public class ConsumableToHoursConvertBaseTest
    {
        private ConsumableToHoursConvertBase _baseSubject;

        public ConsumableToHoursConvertBaseTest()
        {
            _baseSubject = new MyTestableConsumableToHoursConvertBase();
        }

        [Theory]
        [InlineData("1 period", 1, "period")]
        [InlineData("2 periods", 2, "periods")]
        [InlineData("999 xpto", 999, "xpto")]
        public void ParseConsumable_Should_Parse_Quantity_And_Period(string consumable, int expectedQuantity, string expectedPeriod)
        {
            ((MyTestableConsumableToHoursConvertBase)_baseSubject).SetInternalParametrs(0, new List<string> { expectedPeriod });
            _baseSubject.CanConvert(consumable);

            Assert.Equal(expectedPeriod, _baseSubject.Period);
            Assert.Equal(expectedQuantity, _baseSubject.Quantity);
        }

        [Theory]
        [InlineData("1period")]
        [InlineData("1")]
        [InlineData("period")]
        public void ParseConsumable_When_Consumable_Is_Invalid_Throws_ArgumentException(string invalidConsumables)
        {
            var expectedMessage = $"Consumable was unable to parse, converter: MyTestableConsumableToHoursConvertBase, cosumable value: {invalidConsumables}";
            var exception =
                Assert.Throws<ArgumentException>(() => _baseSubject.CanConvert(invalidConsumables));

            Assert.Equal(expectedMessage, exception.Message);
        }
        [Theory]
        [InlineData("1 period", "period", true)]
        [InlineData("2 periods", "period,periods", true)]
        [InlineData("999 xpto", "xpto,xptos", true)]
        [InlineData("999 xpto", "period", false)]
        public void CanConvert_When_Period_Is_Valid_Should_Return_True(string consumable, string validPeriods, bool expectedResult)
        {
            ((MyTestableConsumableToHoursConvertBase)_baseSubject)
                    .SetInternalParametrs(0, validPeriods.Split(',').ToList());

            var result = _baseSubject.CanConvert(consumable);

            Assert.Equal(expectedResult, result);
        }

        [Theory]
        [InlineData("1 week", 7, "week,weeks", 168)]
        [InlineData("1 weeks", 7, "week,weeks", 168)]
        [InlineData("1 month", 30, "month,months", 720)]
        [InlineData("2 months", 30, "month,months", 1440)]
        public void Convert_Should_Return_The_Properly_Values_In_Hours(string consumable, int periodQuantityDays, string validPeriods, int expectedResult)
        {
            ((MyTestableConsumableToHoursConvertBase)_baseSubject)
                    .SetInternalParametrs(periodQuantityDays, validPeriods.Split(',').ToList());

            var result = _baseSubject.Convert(consumable);

            Assert.Equal(expectedResult, result);
        }
    }
}
