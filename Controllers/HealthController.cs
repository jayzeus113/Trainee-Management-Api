using Microsoft.AspNetCore.Mvc;
namespace TraineeManagement.Controllers;

[ApiController]
[Route("/api/[controller]")]
public class HealthCheckController : ControllerBase
{
    [HttpGet(Name = "GetHealth")]
    public IActionResult Get()
    {
        return Ok(
        new {
            status = "running",
            application = "Trainee Management API",
            timestamp = DateTime.UtcNow.ToString("yyyy-MM-ddTHH:mm:ss")
        }
        );
    }
}
