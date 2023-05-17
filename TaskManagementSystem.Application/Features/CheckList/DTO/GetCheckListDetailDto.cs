using TaskManagementSystem.Application.Features.Task.DTO;
using TaskManagementSystem.Domain;

namespace TaskManagementSystem.Application.Features.CheckList.DTO;

public class GetCheckListDetailDto: BaseCheckListDto
{
    public GetUserTaskListDto UserTask { get; set; }

    public Status Status { get; set; }
}
