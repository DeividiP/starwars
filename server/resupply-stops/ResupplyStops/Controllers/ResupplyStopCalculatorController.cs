using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ResupplyStops.Application.Application.Interfaces;
using ResupplyStops.Application.Application.ViewModel;

namespace ResupplyStops.Controllers
{
    [Route("api/resupply-stops")]
    [ApiController]
    public class ResupplyStopCalculatorController : ControllerBase
    {
        private readonly ILogger<ResupplyStopCalculatorController> _logger;
        private readonly IResupllyStopCalculatorService _resupllyStopCalculatorService;

        public ResupplyStopCalculatorController(
            IResupllyStopCalculatorService resupllyStopCalculatorService,
            ILogger<ResupplyStopCalculatorController> logger)
        {
            _logger = logger;
            _resupllyStopCalculatorService = resupllyStopCalculatorService;
        }

        /// <summary>
        /// Retrieve all starships from WSAPI and calculates the number of resupply stops needed to travel a specified distance
        /// </summary>
        /// <response code="200">Retrive all starships stops successfully</response>        
        /// <response code="500">There is an internal server error</response>
        [ProducesResponseType(typeof(StarShipResupplyStopsList),StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [HttpGet]
        public async Task<IActionResult> CalculateAllStarShipsResupplyStopsAsync(int distance)
        {
            _logger.LogInformation($"Initialize CalculateAllStarShipsResupplyStopsAsync", distance);
            try
            {
                var resupllyStops = await _resupllyStopCalculatorService.CalculateAsync(distance);
                _logger.LogInformation($"Finish successfully CalculateAllStarShipsResupplyStopsAsync", resupllyStops);

                return Ok(resupllyStops);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error CalculateAllStarShipsResupplyStopsAsync", distance);
                return StatusCode(StatusCodes.Status500InternalServerError, ex);
            }
        }
    }
}