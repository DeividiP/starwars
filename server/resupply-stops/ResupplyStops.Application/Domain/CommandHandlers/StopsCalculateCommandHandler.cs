using System.Threading.Tasks;

namespace ResupplyStops.Application.Domain.CommandHandlers
{
    public class StopsCalculateCommandHandler : IStopsCalculateCommandHandler
    {
        public async Task<int> Handle(int distance)
        {
            return await Task.FromResult(distance);
        }
    }
}
