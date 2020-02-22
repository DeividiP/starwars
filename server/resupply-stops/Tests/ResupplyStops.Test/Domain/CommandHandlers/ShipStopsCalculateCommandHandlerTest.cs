﻿using Moq;
using ResupplyStops.Application.Domain.CommandHandlers;
using ResupplyStops.Application.Domain.Interfaces;
using ResupplyStops.Application.Domain.Model;
using ResupplyStops.Application.Domain.Query;
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

        [Fact]
        public async Task Should_Call_IWSAPIProxy_GetAllStarShips_Method()
        {
            _wsAPIProxyMock.Setup(_ => _.GetAllStarShips()).Returns(new List<IStarShip>());

            await _subject.Handle(int.MaxValue);

            _wsAPIProxyMock.Verify(wsApi => wsApi.GetAllStarShips(), Times.Once);
        }

        [Fact]
        public async Task Should_Call_StarShip_Calculate_Method_For_Each_StarShip()
        {
            var distance = 99;

            var starShip1Mock = new Mock<IStarShip>();
            var starShip2Mock = new Mock<IStarShip>();
            var starShip3Mock = new Mock<IStarShip>();

            _wsAPIProxyMock.Setup(_ => _.GetAllStarShips())
                            .Returns(new List<IStarShip>()
                                        {
                                            starShip1Mock.Object,
                                            starShip2Mock.Object,
                                            starShip3Mock.Object
                                        });

            await _subject.Handle(distance);

            starShip1Mock.Verify(s => s.CalculateStops(distance), Times.Once);
            starShip2Mock.Verify(s => s.CalculateStops(distance), Times.Once);
            starShip3Mock.Verify(s => s.CalculateStops(distance), Times.Once);
        }

        [Fact]
        public async Task Should_Return_ShipStopsCalculateQuery()
        {
            var distance = 99;
            var mockedStarShip1Stops = 1;
            var mockedStarShip2Stops = 2;
            var mockedStarShip3Stops = 3;

            var starShip1Mock = new Mock<IStarShip>();
            starShip1Mock.Setup(s => s.CalculateStops(distance)).Returns(mockedStarShip1Stops);

            var starShip2Mock = new Mock<IStarShip>();
            starShip2Mock.Setup(s => s.CalculateStops(distance)).Returns(mockedStarShip2Stops);

            var starShip3Mock = new Mock<IStarShip>();
            starShip3Mock.Setup(s => s.CalculateStops(distance)).Returns(mockedStarShip3Stops);

            _wsAPIProxyMock.Setup(_ => _.GetAllStarShips())
                            .Returns(new List<IStarShip>()
                                        {
                                            starShip1Mock.Object,
                                            starShip2Mock.Object,
                                            starShip3Mock.Object
                                        });

            var result = await _subject.Handle(distance);

            Assert.IsType<List<ShipStopsCalculateQuery>>(result);
            Assert.Equal(mockedStarShip1Stops, result[0].Stops);
            Assert.Equal(mockedStarShip2Stops, result[1].Stops);
            Assert.Equal(mockedStarShip3Stops, result[2].Stops);
        }
    }
}
