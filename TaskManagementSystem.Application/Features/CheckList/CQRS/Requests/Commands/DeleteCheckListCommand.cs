using MediatR;
using TaskManagementSystem.Application.Features.CheckList.DTO;

namespace TaskManagementSystem.Application.Features.CheckList.CQRS.Requests.Commands;

public class DeleteCheckListCommand: IRequest
{
    public DeleteCheckListDto deleteCheckListDto {get; set;}
}
