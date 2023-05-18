using TaskManagementSystem.Domain;

namespace TaskManagementSystem.Application.Features.CheckList.DTO;

public class GetCheckListListDto: BaseCheckListDto
{
    public int UserTaskId { get; set; }

    public Status Status { get; set; }
}
