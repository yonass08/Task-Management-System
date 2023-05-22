using TaskManagementSystem.Application.Features.UserTask.CQRS.Requests.Commands;
using TaskManagementSystem.Application.Features.UserTask.CQRS.Requests.Queries;
using TaskManagementSystem.Application.Features.UserTask.DTO;
using Microsoft.AspNetCore.Mvc;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using TaskManagementSystem.Application.Exceptions;

namespace TaskManagementSystem.API.Controllers;

[Authorize]
[ApiController]
[Route("api/[controller]")]
public class UserTaskController : ControllerBase
{
    private readonly IMediator _mediator;
    public UserTaskController(IMediator mediatR)
    {
        _mediator = mediatR;     
    }

    
    [HttpGet]
    public async Task<ActionResult<List<GetUserTaskListDto>>> Get()
    {
        var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        if (userId == null)
            throw new BadRequestException($"missing {userId}");
        var UserTasks = await _mediator.Send(new GetUserTaskListQuery(){UserId = userId});
        return Ok(UserTasks);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<GetUserTaskDetailDto>> Get(int id)
    {
        var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        if (userId == null)
            throw new BadRequestException($"missing {userId}");
        var UserTask = await _mediator.Send(new GetUserTaskDetailQuery { Id = id, UserId = userId});
        return Ok(UserTask);
    }

    [HttpPost]
    public async Task<ActionResult> Post([FromBody] CreateUserTaskDto UserTaskDto)
    {
        var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        if (userId == null)
            throw new BadRequestException($"missing {userId}");
            
        var response = await _mediator.Send(new CreateUserTaskCommand
                {
                    createUserTaskDto = UserTaskDto,
                    UserId = userId
                    });
        return Ok(response);   
    }

    [HttpPut]
    public async Task<ActionResult> Put([FromBody] UpdateUserTaskDto UserTaskDto)
    {
        var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        if (userId == null)
            throw new BadRequestException($"missing {userId}");

        var response = await _mediator.Send(new UpdateUserTaskCommand
                {
                    updateUserTaskDto = UserTaskDto,
                    UserId = userId
                    });
        return NoContent(); 
    }

    [HttpPut("Status")]
    public async Task<ActionResult> Put([FromBody] UpdateUserTaskStatusDto UserTaskDto)
    {
        var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        if (userId == null)
            throw new BadRequestException($"missing {userId}");

        var response = await _mediator.Send(new UpdateUserTaskStatusCommand
                {
                    updateUserTaskStatusDto = UserTaskDto,
                    UserId = userId
                    });
        return NoContent(); 
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> Delete(int id)
    {
        var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        if (userId == null)
            throw new BadRequestException($"missing {userId}");

        var UserTaskDto = new DeleteUserTaskDto
        { 
            Id = id,
        };
        await _mediator.Send(new DeleteUserTaskCommand{ deleteUserTaskDto = UserTaskDto, UserId = userId});
        return NoContent(); 
    }
}

