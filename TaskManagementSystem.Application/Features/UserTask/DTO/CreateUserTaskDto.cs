using TaskManagementSystem.Domain;

namespace TaskManagementSystem.Application.Features.UserTask.DTO;

public class CreateUserTaskDto : BaseUserTaskDto
{
    public string UserId { get; set; }

}