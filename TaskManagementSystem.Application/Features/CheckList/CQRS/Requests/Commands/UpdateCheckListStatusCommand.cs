using MediatR;
using TaskManagementSystem.Application.Features.CheckList.DTO;

namespace TaskManagementSystem.Application.Features.CheckList.CQRS.Requests.Commands;

public class UpdateCheckListStatusCommand: IRequest
{
    public UpdateCheckListStatusDto updateCheckListStatusDto {get; set;}
}
