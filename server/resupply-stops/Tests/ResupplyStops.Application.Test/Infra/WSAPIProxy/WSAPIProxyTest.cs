using Moq;
using ResupplyStops.Application.Domain.Interfaces;
using ResupplyStops.Application.Infra.WSAPIProxy;
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
            _subject = new WSAPIProxy(_iwsApi.Object);
        }

        [Fact]
        public async Task GetAllStarShipsAsync_Should_Agregate_All_Pages_Results()
        {
            _iwsApi.Setup(api => api.GetStarShips(null)).ReturnsAsync(
                    new PageResult<StarShip>
                    {
                        Count = 2,
                        Next = "/?page=2",
                        Previus = null,
                        Result = new List<StarShip>()
                        {
                            new StarShip
                            {
                                Name="Starship 1 from page 1",
                                MGLT=11,
                                Consumable="1 week",
                            },
                            new StarShip
                            {
                                Name="Starship 2 from page 1",
                                MGLT=21,
                                Consumable="1 week",
                            }
                        }
                    }
                );

            _iwsApi.Setup(api => api.GetStarShips(2)).ReturnsAsync(
                    new PageResult<StarShip>
                    {
                        Count = 2,
                        Next = "/?page=3",
                        Previus = "/?page=1",
                        Result = new List<StarShip>()
                        {
                            new StarShip
                            {
                                Name="Starship 1 from page 2",
                                MGLT=12,
                                Consumable="1 week",
                            },
                            new StarShip
                            {
                                Name="Starship 2 from page 2",
                                MGLT=22,
                                Consumable="1 week",
                            }
                        }
                    }
                );

            _iwsApi.Setup(api => api.GetStarShips(3)).ReturnsAsync(
                    new PageResult<StarShip>
                    {
                        Count = 2,
                        Next = null,
                        Previus = "/?page=2",
                        Result = new List<StarShip>()
                        {
                            new StarShip
                            {
                                Name="Starship 1 from page 3",
                                MGLT=13,
                                Consumable="1 week",
                            },
                            new StarShip
                            {
                                Name="Starship 2 from page 3",
                                MGLT=23,
                                Consumable="1 week",
                            }
                        }
                    }
                );

            var result = await _subject.GetAllStarShipsAsync();

            Assert.Equal(6, result.Count);
            Assert.Equal(11, result[0].MGLT);
            Assert.Equal(21, result[0].MGLT);
            Assert.Equal(12, result[0].MGLT);
            Assert.Equal(22, result[0].MGLT);
            Assert.Equal(13, result[0].MGLT);
            Assert.Equal(23, result[0].MGLT);
        }
    }
}
