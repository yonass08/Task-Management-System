using Microsoft.AspNetCore.Mvc;
using TaskManagementSystem.Application.Models.Identity;
using TaskManagementSystem.Application.Contracts.Identity;
using MediatR;
using AutoMapper;

namespace TaskManagementSystem.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly IAuthService _authService;
    private readonly IMediator _mediator;
    private readonly IMapper _mapper;

    public AuthController(IAuthService authService, IMediator mediator, IMapper mapper)
    {
        _authService = authService;
        _mediator = mediator;
        _mapper = mapper;
    }

    [HttpPost]
    [Route("Login")]
    public async Task<ActionResult<LoginResponse>> Login([FromBody] LoginModel request)
    {
        var response =  await _authService.Login(request);
        return Ok(response);
    }

    [HttpPost]
    [Route("Register")]
    public async Task<ActionResult<RegistrationResponse>> Register([FromBody] RegistrationModel registrationModel)
    {
        var response = await _authService.Register(registrationModel);
        return Ok(response);
    }

    [HttpGet]
    [Route("ResendConfirmLink")]
    public async Task<ActionResult<string>> ResendConfirmEmailLink(string email)
    {
        var response = await _authService.sendConfirmEmailLink(email);
        return Ok(response);
    }

    [HttpGet]
    [Route("Confirm")]
    public async Task<ActionResult<string>> ConfirmEmail(string email, string token)
    {
        var response = await _authService.ConfirmEmail(token, email);
        return Ok(response);
        
    }

    [HttpGet]
    [Route("ForgotPassword")]
    public async Task<ActionResult<string>> ForgotPassword(string email)
    {
        var response = await _authService.ForgotPassword(email);
        return Ok(response);
        
    }

    [HttpPost]
    [Route("ResetPassword")]
    public async Task<ActionResult<string>> ResetPassword([FromBody] ResetPasswordModel resetPasswordModel)
    {
        var response = await _authService.ResetPassword(resetPasswordModel);
        return Ok(response);
        
    }

    [HttpDelete]
    [Route("Delete")]
    public async Task<ActionResult<bool>> Delete(string email)
    {
        var response = await _authService.DeleteUser(email);
        return Ok(response);  
    }
}