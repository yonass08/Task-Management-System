using TaskManagementSystem.Application.Models.Identity;

namespace TaskManagementSystem.Application.Contracts.Identity;

public interface IAuthService
{
    public Task<RegistrationResponse> Register(RegistrationModel request);

    public Task<LoginResponse> Login(LoginModel request);

}
