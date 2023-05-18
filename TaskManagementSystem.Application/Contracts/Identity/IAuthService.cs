using TaskManagementSystem.Application.Models.Identity;

namespace TaskManagementSystem.Application.Contracts.Identity;

public interface IAuthService
{
    public Task<RegistrationResponse> Register(RegistrationModel request);

    public Task<LoginResponse> Login(LoginModel request);

    public Task<string> sendConfirmEmailLink(string Email);

    public Task<string> ConfirmEmail(string token, string email);

    public Task<string> ForgotPassword(string Email);

    public Task<string> ResetPassword(ResetPasswordModel resetPasswordModel);

    public Task<bool> DeleteUser(string Email);



}
