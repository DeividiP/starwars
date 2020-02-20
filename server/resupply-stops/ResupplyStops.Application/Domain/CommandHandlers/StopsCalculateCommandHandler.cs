using ResupplyStops.Application.Domain.Interfaces;
using System.Threading.Tasks;

namespace ResupplyStops.Application.Domain.CommandHandlers
{
    public class StopsCalculateCommandHandler : IStopsCalculateCommandHandler
    {
        private readonly IWSAPIProxy _iWSAPIProxy;
        public StopsCalculateCommandHandler(IWSAPIProxy iWSAPIProxy)
        {
            _iWSAPIProxy = iWSAPIProxy;
        }


        public async Task<int> Handle(int distance)
        {
            return await Task.FromResult(distance);
        }
    }
}
