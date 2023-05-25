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

}