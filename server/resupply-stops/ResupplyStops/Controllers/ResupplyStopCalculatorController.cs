using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ResupplyStops.Application.Application.Interfaces;

namespace ResupplyStops.Controllers
{
    [Route("api/resupply-stops")]
    [ApiController]
    public class ResupplyStopCalculatorController : ControllerBase
    {
        private readonly IResupllyStopCalculatorService _resupllyStopCalculatorService;

        public ResupplyStopCalculatorController(
            IResupllyStopCalculatorService resupllyStopCalculatorService)
        {
            _resupllyStopCalculatorService = resupllyStopCalculatorService;
        }

        /// <summary>
        /// Retrieve all starships from WSAPI and calculates the number of resupply stops needed to travel a specified distance
        /// </summary>
        /// <param name="distance">distance used for calculate the number of resuply stops</param>
        /// <response code="200">Retrive all starships stops successfully</response>        
        /// <response code="500">There is an internal server error</response>
        [HttpGet]
        public async Task<IActionResult> CalculateAllStarShipsResupplyStopsAsync(int distance)
        {
            try
            {
                var resupllyStops = await _resupllyStopCalculatorService.CalculateAsync(distance);

                return Ok(resupllyStops);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex);
            }
        }
    }
}