using TaskManagementSystem.Domain;

namespace TaskManagementSystem.Application.Features.CheckList.DTO;

public class UpdateCheckListStatusDto
{
    public int Id {get; set;}

    public Status Status {get; set;}
    
}
