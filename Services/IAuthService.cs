using TraineeManagement.DTOs;
namespace TraineeManagement.Services;

public interface IAuthService
{
    public Task<LoginResponse> Login(LoginRequest loginRequest);
}