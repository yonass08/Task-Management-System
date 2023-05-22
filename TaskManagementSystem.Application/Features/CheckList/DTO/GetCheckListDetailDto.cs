using TaskManagementSystem.Application.Features.UserTask.DTO;
using TaskManagementSystem.Domain;

namespace TaskManagementSystem.Application.Features.CheckList.DTO;

public class GetCheckListDetailDto : BaseCheckListDto
{
    public int UserTaskId { get; set; }

    public GetUserTaskListDto UserTask { get; set; }

    public Status Status { get; set; }
}
