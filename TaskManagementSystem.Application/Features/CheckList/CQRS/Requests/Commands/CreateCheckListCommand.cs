using MediatR;
using TaskManagementSystem.Application.Features.CheckList.DTO;

namespace TaskManagementSystem.Application.Features.CheckList.CQRS.Requests.Commands;

public class CreateCheckListCommand: IRequest<int>
{
    public CreateCheckListDto createCheckListDto {get; set;}

    public string UserId {get; set;}
}
