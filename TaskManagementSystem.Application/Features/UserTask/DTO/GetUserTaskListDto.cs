using TaskManagementSystem.Domain;

namespace TaskManagementSystem.Application.Features.UserTask.DTO;

public class GetUserTaskListDto : BaseUserTaskDto
{
    public string UserId { get; set; }

    public Status Status { get; set; }

}
