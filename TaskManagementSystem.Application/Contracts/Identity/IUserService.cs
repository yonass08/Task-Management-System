using TaskManagementSystem.Application.Models.Identity;

namespace TaskManagementSystem.Application.Contracts.Identity;

   public interface IUserService
    {
        Task<List<User>> GetUsers();
        Task<User?> GetUser(string userId);
        Task<bool> Exists(string userId);

    }
