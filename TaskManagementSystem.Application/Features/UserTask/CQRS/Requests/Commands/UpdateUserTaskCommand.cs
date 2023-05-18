using MediatR;
using TaskManagementSystem.Application.Features.UserTask.DTO;

namespace TaskManagementSystem.Application.Features.UserTask.CQRS.Requests.Commands;

public class UpdateUserTaskCommand: IRequest
{
    public UpdateUserTaskDto updateUserTaskDto {get; set;}
}
