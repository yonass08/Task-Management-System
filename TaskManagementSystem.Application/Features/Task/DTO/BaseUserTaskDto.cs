using TaskManagementSystem.Domain;

namespace TaskManagementSystem.Application.Features.Task.DTO;

public abstract class BaseUserTaskDto
{
    public string Title { get; set; }

    public string Description { get; set; }

    public DateTime StartDate { get; set; }
    
    public DateTime EndDate { get; set; }
}
