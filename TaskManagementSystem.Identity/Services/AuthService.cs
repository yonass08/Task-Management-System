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
    private readonly JwtSettings _jwtSettings;

    public AuthService(UserManager<TaskManagementSystemUser> userManager,
                       SignInManager<TaskManagementSystemUser> signInManager,
                       IOptions<JwtSettings> jwtSettings
                       )
    {
        _userManager = userManager;
        _signInManager = signInManager;
        _jwtSettings = jwtSettings.Value;
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

        return new RegistrationResponse
        {
            UserId = createdUser.Id,
            email = createdUser.Email
        };

    }

   

}
