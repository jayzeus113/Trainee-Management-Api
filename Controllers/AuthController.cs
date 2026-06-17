using Microsoft.AspNetCore.Mvc;
using TraineeManagement.DTOs;
using TraineeManagement.Services;
namespace TraineeManagement.Controllers;

[ApiController]
[Route("/api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly IAuthService _authService;
    public AuthController(IAuthService authService)
    {
        _authService = authService;
    }
    
    [HttpPost("login")]
    public async Task<IActionResult> Login(LoginRequest loginRequest)
    {
        LoginResponse loginResponse = await  _authService.Login(loginRequest);
        return Ok(loginResponse);
    }
}
