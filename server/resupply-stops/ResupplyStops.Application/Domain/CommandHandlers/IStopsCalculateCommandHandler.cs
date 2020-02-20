using System.Threading.Tasks;

namespace ResupplyStops.Application.Domain.CommandHandlers
{
    public interface IStopsCalculateCommandHandler
    {
        Task<int> Handle(int distance);
    }
}
