using TaskManagementSystem.Domain;

namespace TaskManagementSystem.Application.Features.UserTask.DTO;

public class GetUserTaskListDto : BaseUserTaskDto
{
    public int UserId { get; set; }

    public Status Status { get; set; }

}
