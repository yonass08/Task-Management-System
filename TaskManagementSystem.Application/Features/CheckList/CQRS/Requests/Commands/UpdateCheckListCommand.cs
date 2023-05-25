using MediatR;
using TaskManagementSystem.Application.Features.CheckList.DTO;

namespace TaskManagementSystem.Application.Features.CheckList.CQRS.Requests.Commands;

public class UpdateCheckListCommand: IRequest
{
    public UpdateCheckListDto updateCheckListDto {get; set;}

    public string UserId {get; set;}

}
