using Moq;
using ResupplyStops.Application.Domain.CommandHandlers;
using ResupplyStops.Application.Domain.Interfaces;
using ResupplyStops.Application.Domain.Model;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace ResupplyStops.Test.Domain.CommandHandlers
{
    public class ShipStopsCalculateCommandHandlerTest
    {
        readonly IShipStopsCalculateCommandHandler _subject;
        readonly Mock<IWSAPIProxy> _wsAPIProxyMock;

        public ShipStopsCalculateCommandHandlerTest()
        {
            _wsAPIProxyMock = new Mock<IWSAPIProxy>(); 
            _subject = new ShipStopsCalculateCommandHandler(_wsAPIProxyMock.Object);
        }

        [Theory]
        [InlineData("Y-wing", 80, "1 week", 74)]
        [InlineData("Millennium Falcon", 75, "2 months", 9)]
        [InlineData("Rebel Transport", 20, "6 months", 11)]
        public async Task Calculate_Should_Return_Properly_Stops_Based_On_Ships_Data_When_Distance_Is_1000000(string shipName, int mgltPerHour, string consumables, int expectedStops)
        {
            int distance = 1000000;
            var ywing = new StarShip
            {

                Name = shipName,
                MGLT = mgltPerHour,
                Consumables = consumables
            };

            _wsAPIProxyMock.Setup(_ => _.GetAllStarShips()).Returns(new List<StarShip>() { ywing });

            var result = await _subject.Handle(distance);

            Assert.Equal(expectedStops, result[0].Stops);
        }
    }
}
