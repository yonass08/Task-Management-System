using TaskManagementSystem.Application.Features.User.CQRS.Requests.Commands;
using TaskManagementSystem.Application.Features.User.CQRS.Requests.Queries;
using TaskManagementSystem.Application.Features.User.DTO;
using Microsoft.AspNetCore.Mvc;
using MediatR;
using Microsoft.AspNetCore.Authorization;

namespace TaskManagementSystem.API.Controllers;

[Authorize]
[ApiController]
[Route("api/[controller]")]
public class UserController : ControllerBase
{
    private readonly IMediator _mediator;
    public UserController(IMediator mediatR)
    {
        _mediator = mediatR;     
    }

    
    [HttpGet]
    public async Task<ActionResult<List<GetUserListDto>>> Get()
    {
        var Users = await _mediator.Send(new GetUserListQuery());
        return Ok(Users);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<GetUserDetailDto>> Get(int id)
    {
        var User = await _mediator.Send(new GetUserDetailQuery { Id = id });
        return Ok(User);
    }

    [HttpPost]
    public async Task<ActionResult> Post([FromBody] CreateUserDto UserDto)
    {
        var response = await _mediator.Send(new CreateUserCommand{createUserDto = UserDto});
        return Ok(response);   
    }

    [HttpPut]
    public async Task<ActionResult> Put([FromBody] UpdateUserDto UserDto)
    {

        await _mediator.Send(new UpdateUserCommand{updateUserDto = UserDto});
        return NoContent(); 
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> Delete(int id)
    {
        var UserDto = new DeleteUserDto{ Id = id};
        await _mediator.Send(new DeleteUserCommand{ deleteUserDto = UserDto});
        return NoContent(); 
    }
}

