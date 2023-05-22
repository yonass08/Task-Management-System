using MediatR;
using TaskManagementSystem.Application.Features.UserTask.DTO;

namespace TaskManagementSystem.Application.Features.UserTask.CQRS.Requests.Commands;

public class CreateUserTaskCommand: IRequest<int>
{
    public CreateUserTaskDto createUserTaskDto {get; set;}
    public string UserId {get; set;}

}
