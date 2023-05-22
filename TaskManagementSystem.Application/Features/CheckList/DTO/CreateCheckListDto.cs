using TaskManagementSystem.Domain;

namespace TaskManagementSystem.Application.Features.CheckList.DTO;

public class CreateCheckListDto: BaseCheckListDto
{
    public int UserTaskId { get; set; }

    public Status Status {get; set;} = Status.NotStarted;

}
