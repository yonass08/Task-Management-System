using Moq;
using TaskManagementSystem.Application.Contracts.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using TaskManagementSystem.Identity.Models;
using TaskManagementSystem.Application.Models.Identity;
using TaskManagementSystem.Identity.Services;
using System.Security.Claims;
using Shouldly;
using TaskManagementSystem.Application.Exceptions;
using Microsoft.AspNetCore.Http;

namespace TaskManagementSystem.Tests.Services;

public class AuthenticationServiceTests
{
    #region  fields
    private readonly AuthService _authService;
    private readonly Mock<UserManager<TaskManagementSystemUser>> _userManagerMock;
    private readonly Mock<SignInManager<TaskManagementSystemUser>> _signInManagerMock;
    private readonly Mock<IEmailSender> _emailSenderMock;

    #endregion
    public AuthenticationServiceTests()
    {
        _userManagerMock = new Mock<UserManager<TaskManagementSystemUser>>(Mock.Of<IUserStore<TaskManagementSystemUser>>(), null, null, null, null, null, null, null, null);
        _signInManagerMock = new Mock<SignInManager<TaskManagementSystemUser>>(_userManagerMock.Object, Mock.Of<IHttpContextAccessor>(), Mock.Of<IUserClaimsPrincipalFactory<TaskManagementSystemUser>>(), null, null, null, null);
        _emailSenderMock = new Mock<IEmailSender>();

        var jwtOptions =  Options.Create(new JwtSettings() 
        { 
            Key = "2J9JFA9THQTH9AHRHTQ9YAQTJ",
            Issuer = "BlogApi",
            Audience = "BlogApiUser",
            DurationInMinutes = 60
        });

        var serverOptions = Options.Create(new ServerSettings()
        {
            BaseApiUrl = "test"
        });

        _authService = new AuthService(_userManagerMock.Object, _signInManagerMock.Object, jwtOptions);
    }

    #region  Login tests
    [Fact]
    public async Task Login_ShouldReturnAuthResponse_WhenValidCredentialsProvided()
    {
        // Arrange
        var email = "user@example.com";
        var password = "Pa$$w0rd";
        var user = new TaskManagementSystemUser 
        { 
            Email = email, 
            UserName = email
        };
        var authRequest = new LoginModel { Email = email, Password = password };

        _userManagerMock.Setup(u => u.FindByEmailAsync(email))
            .ReturnsAsync(user);
        _userManagerMock.Setup(u => u.GetClaimsAsync(user))
            .ReturnsAsync(new List<Claim>());
        _userManagerMock.Setup(u => u.GetRolesAsync(user))
            .ReturnsAsync(new List<string>());
        _signInManagerMock.Setup(s => s.PasswordSignInAsync(user.UserName, password, false, false))
            .ReturnsAsync(SignInResult.Success);

        // Act
        var result = await _authService.Login(authRequest);

        // Assert
        result.ShouldNotBe(null);
        result.Email.ShouldBe(user.Email);
        result.UserName.ShouldBe(user.UserName);
    
    }

    [Fact]
    public async Task Login_ShouldReturnFalse_WhenUserNotFound()
    {
        // Arrange
        var email = "user@example.com";
        var password = "Pa$$w0rd";
        var authRequest = new LoginModel { Email = email, Password = password };

        _userManagerMock.Setup(u => u.FindByEmailAsync(email))
            .ReturnsAsync((TaskManagementSystemUser)null);

        
        await Should.ThrowAsync<NotFoundException>(async () => 
            await _authService.Login(authRequest)
        );

    }

    [Fact]
    public async Task Login_ShouldReturnFalse_WhenInvalidCredentialsProvided()
    {
        // Arrange
        var email = "user@example.com";
        var password = "Pa$$w0rd";
        var user = new TaskManagementSystemUser { Email = email, UserName = email };
        var authRequest = new LoginModel { Email = email, Password = password };

        _userManagerMock.Setup(u => u.FindByEmailAsync(email))
            .ReturnsAsync(user);
        _signInManagerMock.Setup(s => s.PasswordSignInAsync(user.UserName, password, false, false))
            .ReturnsAsync(SignInResult.Failed);

        await Should.ThrowAsync<BadRequestException>(async () => 
            await _authService.Login(authRequest)
        );

    }

    #endregion


    #region Register tests
    [Fact]
    public async Task Register_ShouldReturnFalse_WhenExistingEmailFound()
    {
        // Arrange
        var request = new RegistrationModel
        {
            Email = "test@example.com",
            Password = "P@ssw0rd",
            UserName = "johndoe",
        };

        _userManagerMock
            .SetupSequence(x => x.FindByNameAsync(request.UserName))
            .ReturnsAsync(new TaskManagementSystemUser());

        _userManagerMock
            .Setup(x => x.FindByEmailAsync(request.Email))
            .ReturnsAsync(new TaskManagementSystemUser());

        // Act & Assert
        await Should.ThrowAsync<BadRequestException>(async () => 
            await _authService.Register(request)
        );
    }

    [Fact]
    public async Task Register_ShouldReturnRegistrationResponse_WhenUserCreatedSuccessfully()
    {
        // Arrange
        var request = new RegistrationModel
        {
            Email = "test@example.com",
            Password = "P@ssw0rd",
            UserName = "johndoe",
        };



        _userManagerMock
            .SetupSequence(x => x.FindByEmailAsync(request.Email))
            .ReturnsAsync((TaskManagementSystemUser)null);

        _userManagerMock
            .SetupSequence(x => x.FindByNameAsync(request.UserName))
            .ReturnsAsync(new TaskManagementSystemUser()
            {
                Email = "test@example.com",
                UserName = "johndoe",
            });


        _userManagerMock
            .Setup(x => x.CreateAsync(It.IsAny<TaskManagementSystemUser>(), request.Password))
            .ReturnsAsync(IdentityResult.Success);


        // Act
        var result = await _authService.Register(request);

        result.ShouldNotBe(null);
        result.email.ShouldBe(request.Email);

    }

    #endregion
}