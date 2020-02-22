using AutoMapper;
using ResupplyStops.Application.Application.Interfaces;
using ResupplyStops.Application.Application.ViewModel;
using ResupplyStops.Application.Domain.Command;
using ResupplyStops.Application.Domain.CommandHandlers;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ResupplyStops.Application.Application.Services
{
    public class ResupllyStopCalculatorService : IResupllyStopCalculatorService
    {
        private readonly IMapper _mapper;
        private readonly IStarShipResupplyStopsCalculateCommandHandler _resupplyStopsCalculateCommandHandler;

        public ResupllyStopCalculatorService(
            IMapper mapper,
            IStarShipResupplyStopsCalculateCommandHandler resupplyStopsCalculateCommandHandler)
        {
            _mapper = mapper;
            _resupplyStopsCalculateCommandHandler = resupplyStopsCalculateCommandHandler;
        }

        public async Task<IList<StarShipResupplyStops>> CalculateAsync(int distance)
        {
            return  _mapper.Map<List<StarShipResupplyStops>>(
                        await _resupplyStopsCalculateCommandHandler.HandleAsync(new StarShipResupplyStopsCalculateCommand() { Distance = distance }));
        }
    }
}
