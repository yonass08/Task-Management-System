using TaskManagementSystem.Domain;

namespace TaskManagementSystem.Application.Features.UserTask.DTO;

public class CreateUserTaskDto : BaseUserTaskDto
{
    public int UserId { get; set; }

}