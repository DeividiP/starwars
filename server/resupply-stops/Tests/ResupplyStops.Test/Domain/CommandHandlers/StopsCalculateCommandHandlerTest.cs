using ResupplyStops.Application.Domain.CommandHandlers;
using System.Threading.Tasks;
using Xunit;

namespace ResupplyStops.Test.Domain.CommandHandlers
{
    public class StopsCalculateCommandHandlerTest
    {
        readonly IStopsCalculateCommandHandler subject;

        public StopsCalculateCommandHandlerTest()
        {
            subject = new StopsCalculateCommandHandler();
        }

        [Fact]
        public async Task Calculate_Should_Return_Zero_When_Distance_Is_Zero()
        {
            int distance = 0;

            var result = await subject.Handle(distance);

            Assert.Equal(0, result);
        }
    }
}
