using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Web;
using TaskManagementSystem.Application.Contracts.Identity;
using TaskManagementSystem.Application.Models.Identity;
using TaskManagementSystem.Application.Models.Mail;
using TaskManagementSystem.Identity.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using TaskManagementSystem.Application.Exceptions;

namespace TaskManagementSystem.Identity.Services;

public class AuthService : IAuthService
{
    private readonly UserManager<TaskManagementSystemUser> _userManager;
    private readonly SignInManager<TaskManagementSystemUser> _signInManager;
    private readonly ServerSettings _serverSettings;
    private readonly IEmailSender _emailSender;
    private readonly JwtSettings _jwtSettings;

    public AuthService(UserManager<TaskManagementSystemUser> userManager,
                       SignInManager<TaskManagementSystemUser> signInManager,
                       IOptions<JwtSettings> jwtSettings,
                       IOptions<ServerSettings> serverSettings,
                       IEmailSender emailSender)
    {
        _userManager = userManager;
        _signInManager = signInManager;
        _serverSettings = serverSettings.Value;
        _emailSender = emailSender;
        _jwtSettings = jwtSettings.Value;
    }
    public async Task<string> ConfirmEmail(string token, string email)
    {
        var user = await _userManager.FindByEmailAsync(email);
        if (user == null)
            throw new NotFoundException(nameof(TaskManagementSystemUser), email);

        var confirmResult = await _userManager.ConfirmEmailAsync(user, token);
        if (!confirmResult.Succeeded)
            throw new BadRequestException("some error");

        return email;

    }

    public async Task<LoginResponse> Login(LoginModel request)
    {

        var user = await _userManager.FindByEmailAsync(request.Email);
        if (user == null)
            throw new NotFoundException(nameof(TaskManagementSystemUser), request.Email);

        var res = await _signInManager.PasswordSignInAsync(user.UserName, request.Password, false, lockoutOnFailure: false);
        if (!res.Succeeded)
            throw new BadRequestException("Incorrect Password");

        JwtSecurityToken token = await GenerateToken(user);

        var response = new LoginResponse()
        {
            UserId = user.Id,
            UserName = user.UserName,
            Email = user.Email,
            Token = new JwtSecurityTokenHandler().WriteToken(token)
        };

        return response;
    }


    private async Task<JwtSecurityToken> GenerateToken(TaskManagementSystemUser user)
    {
        var userClaims = await _userManager.GetClaimsAsync(user);
        var roles = await _userManager.GetRolesAsync(user);

        var roleClaims = new List<Claim>();
        foreach (var role in roles)
        {
            roleClaims.Add(new Claim(ClaimTypes.Role, role));
        }

        var Claims = new List<Claim>()
        {
            new Claim(JwtRegisteredClaimNames.Sub, user.Id),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            new Claim(JwtRegisteredClaimNames.Email, user.Email),
            new Claim(ClaimTypes.NameIdentifier, user.Id)
        }.Union(userClaims)
         .Union(roleClaims);

        var symmetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.Key));
        var signingCredential = new SigningCredentials(symmetricSecurityKey, SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(
            issuer: _jwtSettings.Issuer,
            audience: _jwtSettings.Audience,
            claims: Claims,
            expires: DateTime.UtcNow.AddMinutes(_jwtSettings.DurationInMinutes),
            signingCredentials: signingCredential
        );

        return token;
    }

    public async Task<RegistrationResponse> Register(RegistrationModel request)
    {
        var existingUser = await _userManager.FindByEmailAsync(request.Email);
        if (existingUser != null)
            throw new BadRequestException($"User with given Email({request.Email}) already exists");

        var user = new TaskManagementSystemUser
        {
            UserName = request.UserName,
            FullName = request.FullName,
            Email = request.Email,
            EmailConfirmed = false
        };

        var createResult = await _userManager.CreateAsync(user, request.Password);

        if (!createResult.Succeeded)
        {
            var exception = new BadRequestException("Error occured while creating user");
            foreach (var Error in createResult.Errors)
            {
                exception.Errors.Add(Error.Description);
            }
            throw exception;
        }

        var createdUser = await _userManager.FindByNameAsync(request.UserName);
        await _userManager.AddToRoleAsync(createdUser, "User");

        try
        {
            await sendConfirmEmailLink(createdUser.Email);
        }
        catch(Exception ex)
        {

        }

        return new RegistrationResponse
        {
            UserId = createdUser.Id,
            email = createdUser.Email
        };

    }

    public async Task<string> sendConfirmEmailLink(string Email)
    {
        var user = await _userManager.FindByEmailAsync(Email);
        if (user == null)
            throw new BadRequestException($"User with given Email({Email}) doesn't exists");

        if (user.EmailConfirmed)
            throw new BadRequestException($"User with given Email({Email}) has confirmed the email");

        var token = await _userManager.GenerateEmailConfirmationTokenAsync(user);
        string connectionLink = _serverSettings.BaseApiUrl + $"Auth/Confirm/?email={HttpUtility.UrlEncode(Email)}&token={HttpUtility.UrlEncode(token)}";

        var message = new Email
        {
            To = Email,
            Subject = "Email Confirmation",
            Body = $"Email Confirmation link: {connectionLink}\n token: {token}"
        };

        try
        {
            await _emailSender.sendEmail(message);
        }
        catch(Exception ex)
        {

        }

        return Email;
    }

    public async Task<string> ForgotPassword(string Email)
    {
        var user = await _userManager.FindByEmailAsync(Email);
        if (user == null)
            throw new BadRequestException($"User with given Email({Email}) doesn't exists");
        

        if (!user.EmailConfirmed)
            throw new BadRequestException($"User with given Email({Email}) hasn't confirmed the email");
        

        var token = await _userManager.GeneratePasswordResetTokenAsync(user);
        string resetLink = "# todo: A page where they reset password";

        var message = new Email
        {
            To = Email,
            Subject = "Password Reset",
            Body = $"Password Reset link: {resetLink} \n token: {token}"
        };

        try
        {
            await _emailSender.sendEmail(message);
        }
        catch(Exception ex)
        {

        }

        return Email;
    }

    public async Task<string> ResetPassword(ResetPasswordModel resetPasswordModel)
    {
        var user = await _userManager.FindByEmailAsync(resetPasswordModel.Email);
        if (user == null)
            throw new BadRequestException($"User with given Email({resetPasswordModel.Email}) doesn't exists");

        var resetPassResult = await _userManager.ResetPasswordAsync(user, resetPasswordModel.Token, resetPasswordModel.Password);
        if (!resetPassResult.Succeeded)
        {
            var exception = new BadRequestException("Error occured while creating user");
            foreach (var Error in resetPassResult.Errors)
            {
                exception.Errors.Add(Error.Description);
            }
            throw exception;
        }

        return  resetPasswordModel.Email;
    }

    public async Task<bool> DeleteUser(string Email)
    {
        var user = await _userManager.FindByEmailAsync(Email);
        if (user == null)
            return false;

        var result = await _userManager.DeleteAsync(user);
        return result.Succeeded;
    }

}
