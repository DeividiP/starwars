using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using ResupplyStops.Application.Application.Interfaces;
using ResupplyStops.Application.Application.ViewModel;
using System.Collections.Generic;
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

        public async Task Calculate_Should_Call_CalculateAsync_From_ResupplyStopCalculatorService()
        {
            var dummyDistance = 8;
            _resupllyStopCalculatorServiceMock
                  .Setup(s => s.CalculateAsync(dummyDistance))
                  .ReturnsAsync(new List<StarShipResupplyStops>());

            var response = await _subject.CalculateAllStarShipsResupplyStopsAsync(dummyDistance);

            _resupllyStopCalculatorServiceMock
                .Verify(s => s.CalculateAsync(dummyDistance), Times.Once);
            Assert.Equal(StatusCodes.Status200OK, ((ObjectResult)response).StatusCode);
            Assert.IsType<List<StarShipResupplyStops>>(((ObjectResult)response).Value);
        }
    }
}
