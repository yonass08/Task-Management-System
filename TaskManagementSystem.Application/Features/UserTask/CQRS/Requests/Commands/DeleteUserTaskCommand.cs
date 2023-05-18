using MediatR;
using TaskManagementSystem.Application.Features.UserTask.DTO;

namespace TaskManagementSystem.Application.Features.UserTask.CQRS.Requests.Commands;

public class DeleteUserTaskCommand: IRequest
{
    public DeleteUserTaskDto deleteUserTaskDto {get; set;}
}
