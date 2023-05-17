using TaskManagementSystem.Domain;

namespace TaskManagementSystem.Application.Features.Task.DTO;

public class UpdateUserTaskStatusDto
{
    public int Id {get; set;}

    public Status Status { get; set; }

}
