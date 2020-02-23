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

        public async Task<IActionResult> CalculateAllStarShipsResupplyStopsAsync(int distance)
        {
            var resupllyStops = await _resupllyStopCalculatorService.CalculateAsync(distance);

            return Ok(resupllyStops);
        }
    }
}