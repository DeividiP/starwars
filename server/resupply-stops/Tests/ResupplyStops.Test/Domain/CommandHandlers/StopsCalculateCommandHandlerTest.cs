using Moq;
using ResupplyStops.Application.Domain.CommandHandlers;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace ResupplyStops.Test.Domain.CommandHandlers
{
    public class StopsCalculateCommandHandlerTest
    {
        readonly IStopsCalculateCommandHandler subject;
        readonly IWSAPIProxy wsAPIProxyMock;

        public StopsCalculateCommandHandlerTest()
        {
            wsAPIProxyMock = new Mock<IWSAPIProxy>(); 
            subject = new StopsCalculateCommandHandler(wsAPIProxyMock.Object);
        }

        [Fact]
        public async Task Calculate_Should_Return_Zero_When_Distance_Is_Zero()
        {
            int distance = 0;

            var result = await subject.Handle(distance);

            Assert.Equal(0, result);
        }

        [Theory]
        [InlineData("Y-wing", 80, "1 week", 74)]
        public async Task Calculate_Should_Return_Properly_Stops_Based_On_Ships_Data_When_Distance_Is_1000000(string shipName, int mgltPerHour, string consumables, int expectedStops)
        {
            int distance = 1000000;

            var ywing = new StarShip
            {

                Name = shipName,
                MGLT = mgltPerHour,
                consumables = consumables
            };

            wsAPIProxyMock.Setup(_ => _.GetAllStarShips()).RetunrAysnc(new List<StarShip>() { ywing });

            var result = await subject.Handle(distance);

            Assert.Equal(expectedStops, result);
        }
    }
}
