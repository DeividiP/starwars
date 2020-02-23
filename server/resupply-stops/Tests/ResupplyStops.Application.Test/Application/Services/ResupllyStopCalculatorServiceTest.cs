using AutoMapper;
using ResupplyStops.Application.Application.Interfaces;
using ResupplyStops.Application.Application.Services;
using ResupplyStops.Application.Application.AutoMapperProfiles;
using ResupplyStops.Application.Domain.CommandHandlers;
using Moq;
using ResupplyStops.Application.Domain.Command;
using Xunit;

namespace ResupplyStops.Application.Test.Application.Services
{
    public class ResupllyStopCalculatorServiceTest
    {
        private readonly IResupllyStopCalculatorService _subject;
        private readonly IMapper _mapper;
        private readonly Mock<IStarShipResupplyStopsCalculateCommandHandler> _resupplyStopsCalculateCommandHandlerMock;
        public ResupllyStopCalculatorServiceTest()
        {
            _mapper = new MapperConfiguration(c =>
                    c.AddProfile<DomainToAplicationViewModelMappingProfile>()).CreateMapper();

            _resupplyStopsCalculateCommandHandlerMock = new Mock<IStarShipResupplyStopsCalculateCommandHandler>();

            _subject = new ResupllyStopCalculatorService(
                            _mapper, 
                            _resupplyStopsCalculateCommandHandlerMock.Object);
        }

        [Fact]
        public void Calculate_Should_Call_The_Properly_Command()
        {
            var distance = 99;

            _subject.CalculateAsync(distance);

            _resupplyStopsCalculateCommandHandlerMock
                .Verify(c => c.HandleAsync(It.Is<StarShipResupplyStopsCalculateCommand>(cmd => cmd.Distance == distance)), 
                                Times.Once);
        }
    }
}
