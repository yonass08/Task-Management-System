using TaskManagementSystem.Application.Features.CheckList.CQRS.Requests.Commands;
using TaskManagementSystem.Application.Features.CheckList.CQRS.Requests.Queries;
using TaskManagementSystem.Application.Features.CheckList.DTO;
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
public class CheckListController : ControllerBase
{
    private readonly IMediator _mediator;
    public CheckListController(IMediator mediatR)
    {
        _mediator = mediatR;     
    }

    
    [HttpGet]
    public async Task<ActionResult<List<GetCheckListListDto>>> Get()
    {
        var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        if (userId == null)
            throw new BadRequestException($"missing {userId}");
            
        var Query = new GetCheckListListQuery()
        {
            UserId = userId
        };

        var checkLists = await _mediator.Send(Query);
        return Ok(checkLists);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<GetCheckListDetailDto>> Get(int id)
    {
        var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        if (userId == null)
            throw new BadRequestException($"missing {userId}");

        var Query = new GetCheckListDetailQuery()
        {
            Id = id,
            UserId = userId
        };

        var checkList = await _mediator.Send(Query);
        return Ok(checkList);
    }

    [HttpPost]
    public async Task<ActionResult> Post([FromBody] CreateCheckListDto CheckListDto)
    {
        var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        if (userId == null)
            throw new BadRequestException($"missing {userId}");

        var Command = new CreateCheckListCommand()
        {
            UserId = userId,
            createCheckListDto = CheckListDto
        };

        var response = await _mediator.Send(Command);
        return Ok(response);   
    }

    [HttpPut]
    public async Task<ActionResult> Put([FromBody] UpdateCheckListDto CheckListDto)
    {
        var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        if (userId == null)
            throw new BadRequestException($"missing {userId}");

        var Command = new UpdateCheckListCommand()
        {
            UserId = userId,
            updateCheckListDto = CheckListDto
        };

        var response = await _mediator.Send(Command);
        return NoContent(); 
    }

    [HttpPut("Status")]
    public async Task<ActionResult> Put(int id, string status)
    {
        var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        if (userId == null)
            throw new BadRequestException($"missing {userId}");

        var Command = new UpdateCheckListStatusCommand()
        {
            updateCheckListStatusDto = new UpdateCheckListStatusDto()
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

        var Command = new DeleteCheckListCommand()
        {
            UserId = userId,
            deleteCheckListDto = new DeleteCheckListDto()
            {
                Id = id
            }
        };

        var response = await _mediator.Send(Command);
        return NoContent(); 
    }
}

