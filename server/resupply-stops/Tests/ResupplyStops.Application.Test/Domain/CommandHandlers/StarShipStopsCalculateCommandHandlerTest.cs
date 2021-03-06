﻿using Moq;
using ResupplyStops.Application.Domain.Command;
using ResupplyStops.Application.Domain.CommandHandlers;
using ResupplyStops.Application.Domain.Interfaces;
using ResupplyStops.Application.Domain.Model;
using ResupplyStops.Application.Domain.Query;
using ResupplyStops.Application.Domain.Services;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace ResupplyStops.Application.Test.Domain.CommandHandlers
{
    public class StarShipStopsCalculateCommandHandlerTest
    {
        readonly IStarShipResupplyStopsCalculateCommandHandler _subject;
        readonly Mock<IWSAPIProxy> _wsAPIProxyMock;
        readonly Mock<IResupplyStopsCalculatorService> _resupplyStopsCalculatorService;

        public StarShipStopsCalculateCommandHandlerTest()
        {
            _wsAPIProxyMock = new Mock<IWSAPIProxy>();
            _resupplyStopsCalculatorService = new Mock<IResupplyStopsCalculatorService>();

            _subject = new StarShipResupplyStopsCalculateCommandHandler(
                _wsAPIProxyMock.Object,
                _resupplyStopsCalculatorService.Object);
        }

        [Fact]
        public async Task Should_Call_IWSAPIProxy_GetAllStarShips_Method()
        {
            _wsAPIProxyMock.Setup(_ => _.GetAllStarShipsAsync()).ReturnsAsync(new List<StarShip>());

            await _subject.HandleAsync(new StarShipResupplyStopsCalculateCommand() { Distance = int.MaxValue });

            _wsAPIProxyMock.Verify(wsApi => wsApi.GetAllStarShipsAsync(), Times.Once);
        }

        [Fact]
        public async Task Should_Call_StarShip_Calculate_Method_For_Each_StarShip()
        {
            var command = new StarShipResupplyStopsCalculateCommand() { Distance = 99 };

            var starShip1Mock = new Mock<StarShip>();
            var starShip2Mock = new Mock<StarShip>();
            var starShip3Mock = new Mock<StarShip>();

            _resupplyStopsCalculatorService.Setup(s => s.CalculateStopsAsync(starShip1Mock.Object, command.Distance));
            _resupplyStopsCalculatorService.Setup(s => s.CalculateStopsAsync(starShip1Mock.Object, command.Distance));
            _resupplyStopsCalculatorService.Setup(s => s.CalculateStopsAsync(starShip3Mock.Object, command.Distance));

            _wsAPIProxyMock.Setup(_ => _.GetAllStarShipsAsync())
                            .ReturnsAsync(new List<StarShip>()
                                        {
                                            starShip1Mock.Object,
                                            starShip2Mock.Object,
                                            starShip3Mock.Object
                                        });

            await _subject.HandleAsync(command);

            _resupplyStopsCalculatorService.Verify(s => s.CalculateStopsAsync(starShip1Mock.Object, command.Distance), Times.Once);
            _resupplyStopsCalculatorService.Verify(s => s.CalculateStopsAsync(starShip2Mock.Object, command.Distance), Times.Once);
            _resupplyStopsCalculatorService.Verify(s => s.CalculateStopsAsync(starShip3Mock.Object, command.Distance), Times.Once);
        }

        [Fact]
        public async Task Should_Return_ShipStopsCalculateQuery()
        {
            var command = new StarShipResupplyStopsCalculateCommand() { Distance = 99 };
            var mockedStarShip1Stops = 1;
            var mockedStarShip2Stops = 2;
            var mockedStarShip3Stops = 3;

            var starShip1Mock = new Mock<StarShip>();
            _resupplyStopsCalculatorService.Setup(s => s.CalculateStopsAsync(starShip1Mock.Object, command.Distance))
                                                        .ReturnsAsync(mockedStarShip1Stops);

            var starShip2Mock = new Mock<StarShip>();
            _resupplyStopsCalculatorService.Setup(s => s.CalculateStopsAsync(starShip2Mock.Object, command.Distance))
                                                        .ReturnsAsync(mockedStarShip2Stops);

            var starShip3Mock = new Mock<StarShip>();
            _resupplyStopsCalculatorService.Setup(s => s.CalculateStopsAsync(starShip3Mock.Object, command.Distance))
                                                        .ReturnsAsync(mockedStarShip3Stops);

            _wsAPIProxyMock.Setup(_ => _.GetAllStarShipsAsync())
                            .ReturnsAsync(new List<StarShip>()
                                        {
                                            starShip1Mock.Object,
                                            starShip2Mock.Object,
                                            starShip3Mock.Object
                                        });

            var result = await _subject.HandleAsync(command);

            Assert.IsType<List<ShipStopsCalculateQuery>>(result);
            Assert.Equal(mockedStarShip1Stops, result[0].Stops);
            Assert.Equal(mockedStarShip2Stops, result[1].Stops);
            Assert.Equal(mockedStarShip3Stops, result[2].Stops);
        }

        [Fact]
        public async Task Should_Thrown_ArgumentOutOfRangeException_When_Distance_Is_Less_Than_Zero()
        {
            var command = new StarShipResupplyStopsCalculateCommand() { Distance = -1 };
            var expectedExceptionMessage = "The distance must be greater than zero. (Parameter 'distance')";

            var result = await Assert.ThrowsAsync<ArgumentOutOfRangeException>(() => _subject.HandleAsync(command));

            Assert.Equal(expectedExceptionMessage, result.Message);
        }
    }
}
