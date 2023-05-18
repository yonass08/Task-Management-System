using Microsoft.EntityFrameworkCore;
using TaskManagementSystem.Application.Contracts.Persistence;
using TaskManagementSystem.Domain;

namespace TaskManagementSystem.Persistence.Repositories;

public class CheckListRepository : GenericRepository<CheckList>, ICheckListRepository
{

    private readonly TaskManagementSystemDbContext _dbContext;
    public CheckListRepository(TaskManagementSystemDbContext context) : base(context)
    {
        _dbContext = context;
    }

    public Task UpdateStatus(CheckList checkList, Status status)
    {
        throw new NotImplementedException();
    }
}
