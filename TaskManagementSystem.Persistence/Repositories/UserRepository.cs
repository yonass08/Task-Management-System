using Microsoft.EntityFrameworkCore;
using TaskManagementSystem.Application.Contracts.Persistence;
using TaskManagementSystem.Domain;

namespace TaskManagementSystem.Persistence.Repositories;

public class UserRepository : GenericRepository<User>, IUserRepository
{

    private readonly TaskManagementSystemDbContext _dbContext;
    public UserRepository(TaskManagementSystemDbContext context) : base(context)
    {
        _dbContext = context;
    }
    
    public Task<User?> GetByEmail(string Email)
    {
        return _dbContext.Users.FirstOrDefaultAsync( user => user.Email == Email);
    }
}
