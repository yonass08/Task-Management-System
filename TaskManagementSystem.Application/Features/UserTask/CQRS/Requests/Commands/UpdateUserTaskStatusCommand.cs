using MediatR;
using TaskManagementSystem.Application.Features.UserTask.DTO;

namespace TaskManagementSystem.Application.Features.UserTask.CQRS.Requests.Commands;

public class UpdateUserTaskStatusCommand: IRequest
{
    public UpdateUserTaskStatusDto updateUserTaskStatusDto {get; set;}

    public string UserId {get; set;}

}
