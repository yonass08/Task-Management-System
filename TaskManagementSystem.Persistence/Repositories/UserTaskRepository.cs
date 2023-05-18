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

    public Task UpdateStatus(UserTask userTask, Status status)
    {
        throw new NotImplementedException();
    }
}
