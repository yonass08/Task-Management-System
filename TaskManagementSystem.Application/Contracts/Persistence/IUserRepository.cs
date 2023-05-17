using TaskManagementSystem.Domain;

namespace TaskManagementSystem.Application.Contracts.Persistence;

public interface IUserRepository: IGenericRepository<User>
{
    Task<User> GetByEmail(string Email);

}
