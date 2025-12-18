using LiftLedger.Services.Models;
using LiftLedger.Services.Services;
using Microsoft.AspNetCore.Mvc;

namespace LiftLedger.Services.Controllers;

[ApiController]
[Route("api/[controller]")]
public class InsightsController : ControllerBase
{
    private readonly InsightsService _insightsService;
    private readonly ILogger<InsightsController> _logger;

    public InsightsController(InsightsService insightsService, ILogger<InsightsController> logger)
    {
        _insightsService = insightsService;
        _logger = logger;
    }

    [HttpPost("progress")]
    public IActionResult AnalyzeProgress([FromBody] ProgressRequest request)
    {
        if (request == null || request.History == null || request.History.Count == 0)
        {
            _logger.LogWarning("Invalid request received: empty or null history");
            return BadRequest(new { error = "Invalid request. History cannot be empty." });
        }

        try
        {
            var insight = _insightsService.AnalyzeProgress(request);
            return Ok(insight);
        }
        catch (ArgumentException ex)
        {
            _logger.LogWarning(ex, "Invalid argument in progress analysis");
            return BadRequest(new { error = ex.Message });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Unexpected error processing progress analysis");
            return StatusCode(500, new { error = "An error occurred while processing your request." });
        }
    }
}