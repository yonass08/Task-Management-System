using MediatR;
using TaskManagementSystem.Application.Features.UserTask.DTO;

namespace TaskManagementSystem.Application.Features.UserTask.CQRS.Requests.Queries;

public class GetUserTaskDetailQuery: IRequest<GetUserTaskDetailDto>
{
    public int Id {get; set;}

    public string UserId {get; set;}
    
}
