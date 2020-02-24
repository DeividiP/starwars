using ResupplyStops.Application.Domain.Interfaces;
using ResupplyStops.Application.Domain.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ResupplyStops.Application.Infra.WSAPIProxy
{
    public class WSAPIProxy : IWSAPIProxy
    {
        private readonly IWSApi _wSApi;

        public WSAPIProxy(IWSApi wSApi)
        {
            _wSApi = wSApi;
        }

        public async Task<List<StarShip>> GetAllStarShipsAsync()
        {
            var firstPage = await _wSApi.GetStarShipsAsync(1);
            var result = firstPage.Results;

            result.AddRange(await GetNextStarShipPage(firstPage, result));

            return result;
        }

        private async Task<List<StarShip>> GetNextStarShipPage(PageResult<StarShip> currentPage, List<StarShip> cumulatedResult)
        {
            if (currentPage.NextPage == null)
                return new List<StarShip>();

            var nextPage = await _wSApi.GetStarShipsAsync(currentPage.NextPage);
            cumulatedResult.AddRange(nextPage.Results);

            return await GetNextStarShipPage(nextPage, cumulatedResult);
        }
    }
}
