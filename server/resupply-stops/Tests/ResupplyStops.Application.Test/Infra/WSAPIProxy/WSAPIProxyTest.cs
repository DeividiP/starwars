using Moq;
using ResupplyStops.Application.Domain.Interfaces;
using ResupplyStops.Application.Domain.Model;
using ResupplyStops.Application.Infra.WSAPIProxy;
using InfraWSAPIProxy = ResupplyStops.Application.Infra.WSAPIProxy;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace ResupplyStops.Application.Test.Infra.WSAPIProxy
{
    public class WSAPIProxyTest
    {
        private readonly Mock<IWSApi> _iwsApi;
        private readonly IWSAPIProxy _subject;

        public WSAPIProxyTest()
        {
            _iwsApi = new Mock<IWSApi>();
            _subject = new InfraWSAPIProxy.WSAPIProxy(_iwsApi.Object);
        }

        [Fact]
        public async Task GetAllStarShipsAsync_Should_Agregate_All_Pages_Results()
        {
            SetupGetStarShipsAsyncPage(1, 2);
            SetupGetStarShipsAsyncPage(2, 3);
            SetupGetStarShipsAsyncPage(3);

            var result = await _subject.GetAllStarShipsAsync();

            Assert.Equal(6, result.Count);
            Assert.Equal("Starship 11", result[0].Name);
            Assert.Equal("Starship 21", result[1].Name);
            Assert.Equal("Starship 12", result[2].Name);
            Assert.Equal("Starship 22", result[3].Name);
            Assert.Equal("Starship 13", result[4].Name);
            Assert.Equal("Starship 23", result[5].Name);
        }

        private void SetupGetStarShipsAsyncPage(int page, int? nextPage = null)
        {
            _iwsApi.Setup(api => api.GetStarShipsAsync(page)).ReturnsAsync(
                new PageResult<StarShip>
                {
                    Count = 2,
                    Next = nextPage.HasValue? $"/?page={nextPage.Value}" : null,
                    Results = new List<StarShip>()
                    {
                        new StarShip
                        {
                            Name=$"Starship 1{page}",
                        },
                        new StarShip
                        {
                            Name=$"Starship 2{page}",
                        }
                    }
                }
            );
        }
    }
}
