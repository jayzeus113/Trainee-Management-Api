using TraineeManagement.Models;
using TraineeManagement.DTOs;
using TraineeManagement.Data;
using TraineeManagement.Exceptions;
using Microsoft.EntityFrameworkCore;

namespace TraineeManagement.Services;

public class AuthService : IAuthService
{
    private readonly AppDbContext _context;

    private readonly JwtService _jwtService;

    private readonly ILogger<TraineeService> _logger;

    public AuthService(AppDbContext context, JwtService jwtService, ILogger<TraineeService> logger)
    {
        _context = context;
        _jwtService = jwtService;
        _logger = logger;
    }

    public async Task<LoginResponse> Login(LoginRequest loginRequest)
    {
        User? user = await _context.Users.FirstOrDefaultAsync(u => u.UserName == loginRequest.UserName);
        if(user == null)
        {
            _logger.LogWarning("Record not found. Resource: {ResourceType}, Username: {Identifier}", "Trainee", loginRequest.UserName);
            throw new UnauthorizedException("Invalid Username or password");
        }

        bool isValidPassword = PasswordHasherService.VerifyPassword(loginRequest.Password, user.PasswordHash);
        if(!isValidPassword)
        {
            _logger.LogWarning("Password is Incorrect. Resource: {ResourceType}, Username: {Identifier}", "Trainee", loginRequest.UserName);
            throw new UnauthorizedException("Invalid Username or password");
        }

        var token = _jwtService.GenerateToken(user.Id, user.UserName, user.Role);

        return new LoginResponse
        {
            Token = token,
            ExpiresIn = 3600,
            User = new UserResponse
            {
                Id = user.Id,
                Username = user.UserName,
                Role = user.Role
            }
        };
    }
}