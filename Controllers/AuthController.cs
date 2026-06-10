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
    
    [HttpPost(Name = "/login")]
    public async Task<IActionResult> Login(LoginRequest loginRequest)
    {
        LoginResponse? loginResponse = await  _authService.Login(loginRequest);
        return loginResponse == null ? Unauthorized("Username or Password is Incorrect!") : Ok(loginResponse);
    }
}
