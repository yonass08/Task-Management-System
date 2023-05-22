using TaskManagementSystem.Domain;

namespace TaskManagementSystem.Application.Contracts.Identity;

public interface IAuthorizationService
{
    Task<bool> UserTaskBelongsToUser(UserTask userTask, string userId);
    
    Task<bool> UserCanCreatTask(string CreatorId, string CreatedId);

    Task<bool> CheckListBelongsToUser(CheckList checkList, string userId);

    Task<bool> IsAdmin(string userId);


}

