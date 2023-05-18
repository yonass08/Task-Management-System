using TaskManagementSystem.Application.Features.CheckList.CQRS.Requests.Commands;
using TaskManagementSystem.Application.Features.CheckList.CQRS.Requests.Queries;
using TaskManagementSystem.Application.Features.CheckList.DTO;
using Microsoft.AspNetCore.Mvc;
using MediatR;

namespace TaskManagementSystem.API.Controllers;

 
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
        var checkLists = await _mediator.Send(new GetCheckListListQuery());
        return Ok(checkLists);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<GetCheckListDetailDto>> Get(int id)
    {
        var checkList = await _mediator.Send(new GetCheckListDetailQuery { Id = id });
        return Ok(checkList);
    }

    [HttpPost]
    public async Task<ActionResult> Post([FromBody] CreateCheckListDto CheckListDto)
    {
        var response = await _mediator.Send(new CreateCheckListCommand{createCheckListDto = CheckListDto});
        return Ok(response);   
    }

    [HttpPut]
    public async Task<ActionResult> Put([FromBody] UpdateCheckListDto CheckListDto)
    {

        await _mediator.Send(new UpdateCheckListCommand{updateCheckListDto = CheckListDto});
        return NoContent(); 
    }

    [HttpPut("Status")]
    public async Task<ActionResult> Put([FromBody] UpdateCheckListStatusDto CheckListDto)
    {

        await _mediator.Send(new UpdateCheckListStatusCommand{updateCheckListStatusDto = CheckListDto});
        return NoContent(); 
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> Delete(int id)
    {
        var CheckListDto = new DeleteCheckListDto{ Id = id};
        await _mediator.Send(new DeleteCheckListCommand{ deleteCheckListDto = CheckListDto});
        return NoContent(); 
    }
}

