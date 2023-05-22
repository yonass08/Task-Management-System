using Microsoft.EntityFrameworkCore;
using TaskManagementSystem.Application.Contracts.Persistence;
using TaskManagementSystem.Domain;

namespace TaskManagementSystem.Persistence.Repositories;

public class UserTaskRepository : GenericRepository<UserTask>, IUserTaskRepository
{

    private readonly TaskManagementSystemDbContext _dbContext;
    public UserTaskRepository(TaskManagementSystemDbContext context) : base(context)
    {
        _dbContext = context;
    }

    public async Task<UserTask> GetWithDetails(int id)
    {
        return await _dbContext.UserTasks
            .Include(u => u.CheckLists)
            .FirstOrDefaultAsync(u => u.Id == id);

    }

    public Task UpdateStatus(UserTask userTask, Status status)
    {
        throw new NotImplementedException();
    }
}
