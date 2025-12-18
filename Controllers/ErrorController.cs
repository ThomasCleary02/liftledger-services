using Microsoft.AspNetCore.Mvc;

namespace LiftLedger.Services.Controllers;

[ApiController]
public class ErrorController : ControllerBase
{
    [Route("/error")]
    public IActionResult Error()
    {
        return Problem(
            title: "An error occurred while processing your request.",
            statusCode: 500
        );
    }
}