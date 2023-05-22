using TaskManagementSystem.Application.Features.CheckList.DTO;
using TaskManagementSystem.Domain;

namespace TaskManagementSystem.Application.Features.UserTask.DTO;

public class GetUserTaskDetailDto : BaseUserTaskDto
{
    public string UserId { get; set; }
    
    public List<GetCheckListListDto> CheckLists {get; set;}

    public Status Status { get; set; }

}
