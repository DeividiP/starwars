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
            _iwsApi.Setup(_ => _.GetStarShipsAsync(null)).ReturnsAsync(
                    new PageResult<StarShip>
                    {
                        Count = 2,
                        Next = "/?page=2",
                        Previous = null,
                        Result = new List<StarShip>()
                        {
                            new StarShip
                            {
                                Name="Starship 1 from page 1",
                                MGLT=11,
                                Consumables="1 week",
                            },
                            new StarShip
                            {
                                Name="Starship 2 from page 1",
                                MGLT=21,
                                Consumables="1 week",
                            }
                        }
                    }
                );

            _iwsApi.Setup(api => api.GetStarShipsAsync(2)).ReturnsAsync(
                    new PageResult<StarShip>
                    {
                        Count = 2,
                        Next = "/?page=3",
                        Previous = "/?page=1",
                        Result = new List<StarShip>()
                        {
                            new StarShip
                            {
                                Name="Starship 1 from page 2",
                                MGLT=12,
                                Consumables="1 week",
                            },
                            new StarShip
                            {
                                Name="Starship 2 from page 2",
                                MGLT=22,
                                Consumables="1 week",
                            }
                        }
                    }
                );

            _iwsApi.Setup(api => api.GetStarShipsAsync(3)).ReturnsAsync(
                    new PageResult<StarShip>
                    {
                        Count = 2,
                        Next = null,
                        Previous = "/?page=2",
                        Result = new List<StarShip>()
                        {
                            new StarShip
                            {
                                Name="Starship 1 from page 3",
                                MGLT=13,
                                Consumables="1 week",
                            },
                            new StarShip
                            {
                                Name="Starship 2 from page 3",
                                MGLT=23,
                                Consumables="1 week",
                            }
                        }
                    }
                );

            var result = await _subject.GetAllStarShipsAsync();

            Assert.Equal(6, result.Count);
            Assert.Equal(11, result[0].MGLT);
            Assert.Equal(21, result[1].MGLT);
            Assert.Equal(12, result[2].MGLT);
            Assert.Equal(22, result[3].MGLT);
            Assert.Equal(13, result[4].MGLT);
            Assert.Equal(23, result[5].MGLT);
        }
    }
}
