using Microsoft.AspNetCore.Identity;
using TaskManagementSystem.Application.Contracts.Identity;
using TaskManagementSystem.Application.Contracts.Persistence;
using TaskManagementSystem.Domain;
using TaskManagementSystem.Identity.Models;

public class AuthorizationService : IAuthorizationService
{

    private readonly UserManager<TaskManagementSystemUser> _userManager;

    public AuthorizationService(UserManager<TaskManagementSystemUser> userManager)
    {
        _userManager = userManager;

    }

    public async Task<bool> UserTaskBelongsToUser(UserTask userTask, string userId)
    {
        return userTask.UserId == userId;
    }

    public async Task<bool> CheckListBelongsToUser(CheckList checkList, string userId)
    {
        var expected = checkList.UserTask.UserId;
        return expected == userId;
    }

    public async Task<bool> IsAdmin(string userId)
    {
        var user = await _userManager.FindByIdAsync(userId);
        return await _userManager.IsInRoleAsync(user, "Admin");
    }

    public async Task<bool> UserCanCreatTask(string CreatorId, string CreatedId)
    {
        return CreatedId == CreatorId;
    }
}
