using TaskManagementSystem.Domain;

namespace TaskManagementSystem.Application.Features.Task.DTO;

public class GetUserTaskListDto: BaseUserTaskDto
{
    public int UserId { get; set; }

    public Status Status { get; set; }

}
