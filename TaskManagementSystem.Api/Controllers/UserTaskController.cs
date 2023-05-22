using TaskManagementSystem.Application.Features.UserTask.CQRS.Requests.Commands;
using TaskManagementSystem.Application.Features.UserTask.CQRS.Requests.Queries;
using TaskManagementSystem.Application.Features.UserTask.DTO;
using Microsoft.AspNetCore.Mvc;
using MediatR;
using Microsoft.AspNetCore.Authorization;

namespace TaskManagementSystem.API.Controllers;

// [Authorize]
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
        var UserTasks = await _mediator.Send(new GetUserTaskListQuery());
        return Ok(UserTasks);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<GetUserTaskDetailDto>> Get(int id)
    {
        var UserTask = await _mediator.Send(new GetUserTaskDetailQuery { Id = id });
        return Ok(UserTask);
    }

    [HttpPost]
    public async Task<ActionResult> Post([FromBody] CreateUserTaskDto UserTaskDto)
    {
        var response = await _mediator.Send(new CreateUserTaskCommand{createUserTaskDto = UserTaskDto});
        return Ok(response);   
    }

    [HttpPut]
    public async Task<ActionResult> Put([FromBody] UpdateUserTaskDto UserTaskDto)
    {

        await _mediator.Send(new UpdateUserTaskCommand{updateUserTaskDto = UserTaskDto});
        return NoContent(); 
    }

    [HttpPut("Status")]
    public async Task<ActionResult> Put([FromBody] UpdateUserTaskStatusDto UserTaskDto)
    {

        await _mediator.Send(new UpdateUserTaskStatusCommand{updateUserTaskStatusDto = UserTaskDto});
        return NoContent(); 
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> Delete(int id)
    {
        var UserTaskDto = new DeleteUserTaskDto{ Id = id};
        await _mediator.Send(new DeleteUserTaskCommand{ deleteUserTaskDto = UserTaskDto});
        return NoContent(); 
    }
}

