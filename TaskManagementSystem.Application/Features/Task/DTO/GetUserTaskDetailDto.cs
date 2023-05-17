using TaskManagementSystem.Application.Features.User.DTO;
using TaskManagementSystem.Domain;

namespace TaskManagementSystem.Application.Features.Task.DTO;

public class GetUserTaskDetailDto: BaseUserTaskDto
{
    public GetUserListDto User {get; set;}
    
    public Status Status { get; set; }

}
