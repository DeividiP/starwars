using Moq;
using ResupplyStops.Application.Domain.Services;
using System.Collections.Generic;
using Xunit;

namespace ResupplyStops.Test.Domain.Services
{
    public class ConsumablesConvertServiceTest
    {
        private readonly IConsumablesConvertService _subject;
        private readonly IList<IConsumableToHoursConvert> _toHourConverters;
        private Mock<IConsumableToHoursConvert> _converter1;
        private Mock<IConsumableToHoursConvert> _converter2;

        public ConsumablesConvertServiceTest()
        {
            _converter1 = new Mock<IConsumableToHoursConvert>();
            _converter2 = new Mock<IConsumableToHoursConvert>();

            _toHourConverters = new List<IConsumableToHoursConvert>()
            {
                _converter1.Object,
                _converter2.Object,
            };

            _subject = new ConsumablesConvertService(_toHourConverters);
        }

        [Fact]
        public void ConvertToHours_Should_Try_All_Converters()
        {
            var consumableDummyValue = "dummy consumable";
            SetupConvertersToCannotConvert();

            _subject.ConvertToHours(consumableDummyValue);

            _converter1.Verify(c => c.CanConvert(consumableDummyValue));
            _converter2.Verify(c => c.CanConvert(consumableDummyValue));
        }

        [Fact]
        public void ConvertToHours_When_Any_Convert_Match_Should_Return_Zero()
        {
            SetupConvertersToCannotConvert();

            var result = _subject.ConvertToHours("dummy consumable");

            Assert.Equal(0, result);
        }

        [Fact]
        public void ConvertToHours_When_Convert_Match_Should_Return_Convert_Result()
        {
            var convert2MockedResult = 9876;

            _converter1.Setup(c => c.CanConvert(It.IsAny<string>()))
                                    .Returns(false);
            _converter2.Setup(c => c.CanConvert(It.IsAny<string>()))
                                    .Returns(true);
            _converter2.Setup(c => c.Convert(It.IsAny<string>()))
                                    .Returns(convert2MockedResult);

            var result = _subject.ConvertToHours("dummy consumable");

            Assert.Equal(convert2MockedResult, result);
        }

        private void SetupConvertersToCannotConvert()
        {
            _converter1.Setup(c => c.CanConvert(It.IsAny<string>()))
                                    .Returns(false);
            _converter2.Setup(c => c.CanConvert(It.IsAny<string>()))
                                    .Returns(false);
        }
    }
}
