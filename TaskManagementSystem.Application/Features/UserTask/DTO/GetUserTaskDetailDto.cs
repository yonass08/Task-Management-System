using TaskManagementSystem.Application.Features.User.DTO;
using TaskManagementSystem.Domain;

namespace TaskManagementSystem.Application.Features.UserTask.DTO;

public class GetUserTaskDetailDto : BaseUserTaskDto
{
    public int UserId { get; set; }
    
    public Status Status { get; set; }

}
