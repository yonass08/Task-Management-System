using TaskManagementSystem.Application.Features.UserTask.CQRS.Requests.Commands;
using TaskManagementSystem.Application.Features.UserTask.CQRS.Requests.Queries;
using TaskManagementSystem.Application.Features.UserTask.DTO;
using Microsoft.AspNetCore.Mvc;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using TaskManagementSystem.Application.Exceptions;
using TaskManagementSystem.Domain;

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

        var Query = new GetUserTaskListQuery()
        {
            UserId = userId
        };

        var UserTasks = await _mediator.Send(Query);
        return Ok(UserTasks);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<GetUserTaskDetailDto>> Get(int id)
    {
        var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        if (userId == null)
            throw new BadRequestException($"missing {userId}");

        var Query = new GetUserTaskDetailQuery()
        {
            UserId = userId,
            Id = id
        };

        var UserTask = await _mediator.Send(Query);
        return Ok(UserTask);
    }

    [HttpPost]
    public async Task<ActionResult> Post([FromBody] CreateUserTaskDto UserTaskDto)
    {
        var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        if (userId == null)
            throw new BadRequestException($"missing {userId}");
            
        var Command = new CreateUserTaskCommand()
        {
            createUserTaskDto = UserTaskDto,
            UserId = userId
        };
        
        var response = await _mediator.Send(Command);
        return Ok(response);   
    }

    [HttpPut]
    public async Task<ActionResult> Put([FromBody] UpdateUserTaskDto UserTaskDto)
    {
        var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        if (userId == null)
            throw new BadRequestException($"missing {userId}");

        var Command = new UpdateUserTaskCommand()
        {
            updateUserTaskDto = UserTaskDto,
            UserId = userId
        };
        
        var response = await _mediator.Send(Command);
        return NoContent(); 
    }

    [HttpPut("Status")]
    public async Task<ActionResult> Put(int id,  string status)
    {
        var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        if (userId == null)
            throw new BadRequestException($"missing {userId}");

        var Command = new UpdateUserTaskStatusCommand()
        {
            updateUserTaskStatusDto = new UpdateUserTaskStatusDto()
            {
                Id = id,
                Status = (Status)Enum.Parse(typeof(Status), status)
            },
            UserId = userId
        };
        
        var response = await _mediator.Send(Command);
        return NoContent(); 
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> Delete(int id)
    {
        var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        if (userId == null)
            throw new BadRequestException($"missing {userId}");

        var Command = new DeleteUserTaskCommand()
        {
            UserId = userId,
            deleteUserTaskDto = new DeleteUserTaskDto()
            {
                Id = id
            }
        };
        
        var response = await _mediator.Send(Command);
        return NoContent(); 
    }
}

