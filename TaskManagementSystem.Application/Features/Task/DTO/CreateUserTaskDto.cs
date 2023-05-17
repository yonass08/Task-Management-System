using TaskManagementSystem.Domain;

namespace TaskManagementSystem.Application.Features.Task.DTO;

public class CreateUserTaskDto: BaseUserTaskDto
{
    public int UserId { get; set; }

}