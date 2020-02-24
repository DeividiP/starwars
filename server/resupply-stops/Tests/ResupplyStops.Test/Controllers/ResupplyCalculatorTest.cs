using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using ResupplyStops.Application.Application.Interfaces;
using ResupplyStops.Application.Application.ViewModel;
using ResupplyStops.Controllers;
using System;
using System.Threading.Tasks;
using Xunit;

namespace ResupplyStops.Test.Controllers
{
    public class ResupplyCalculatorTest
    {
        private readonly ResupplyStopCalculatorController _subject;
        private readonly Mock<IResupllyStopCalculatorService> _resupllyStopCalculatorServiceMock;

        public ResupplyCalculatorTest()
        {
            _resupllyStopCalculatorServiceMock = new Mock<IResupllyStopCalculatorService>();
            _subject = new ResupplyStopCalculatorController(_resupllyStopCalculatorServiceMock.Object);
        }

        [Fact]
        public async Task Calculate_Should_Call_CalculateAsync_From_ResupplyStopCalculatorService()
        {
            var dummyDistance = 8;
            _resupllyStopCalculatorServiceMock
                  .Setup(s => s.CalculateAsync(dummyDistance))
                  .ReturnsAsync(new StarShipResupplyStopsList());

            var response = await _subject.CalculateAllStarShipsResupplyStopsAsync(dummyDistance);

            _resupllyStopCalculatorServiceMock
                .Verify(s => s.CalculateAsync(dummyDistance), Times.Once);
            Assert.Equal(StatusCodes.Status200OK, ((ObjectResult)response).StatusCode);
            Assert.IsType<StarShipResupplyStopsList>(((ObjectResult)response).Value);
        }

        [Fact]
        public async Task Calculate_Should_Return_HTTP500_When_Internal_Error()
        {
            var dummyDistance = 8;
            _resupllyStopCalculatorServiceMock
                  .Setup(s => s.CalculateAsync(dummyDistance))
                  .ThrowsAsync(new Exception());

            var response = await _subject.CalculateAllStarShipsResupplyStopsAsync(dummyDistance);

            Assert.Equal(StatusCodes.Status500InternalServerError, ((ObjectResult)response).StatusCode);
        }
    }
}
