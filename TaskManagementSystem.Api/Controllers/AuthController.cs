using Microsoft.AspNetCore.Mvc;
using TaskManagementSystem.Application.Models.Identity;
using TaskManagementSystem.Application.Contracts.Identity;
using TaskManagementSystem.Application.Features.User.CQRS.Requests.Commands;
using TaskManagementSystem.Application.Features.User.DTO;
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
    public async Task<ActionResult<RegistrationResponse>> Register([FromBody] RegisterDto registerDto)
    {
        var response = await _authService.Register(_mapper.Map<RegistrationModel>(registerDto));
        var command = new CreateUserCommand {createUserDto = _mapper.Map<CreateUserDto>(registerDto)};

        try
        {
            var userResponse = await _mediator.Send(command);
            return Ok(response);
        }
        catch(Exception e)
        {
            await _authService.DeleteUser(registerDto.Email);
            throw e;
        }
        
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