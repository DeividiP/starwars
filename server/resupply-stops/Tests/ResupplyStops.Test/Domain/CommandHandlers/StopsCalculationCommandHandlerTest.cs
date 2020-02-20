using System;
using Xunit;

namespace ResupplyStops.Test.Domain.CommandHandlers
{
    public class StopsCalculationCommandHandlerTest
    {
        readonly IStopsCalculationCommandHandler subject;

        public StopsCalculationCommandHandlerTest()
        {
            subject = new StopsCalculationCommandHandler();
        }

        [Fact]
        public void Calculate_Should_Throw_ArgumentnullExpection_When_Distance_Is_Null()
        {
            Assert.ThrowsAsync<ArgumentNullException>(subject.Calculate(null))
        }
    }
}
