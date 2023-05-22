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

    public async Task<CheckList> GetWithDetails(int id)
    {
        return await _dbContext.CheckLists.Include(c => c.UserTask).FirstOrDefaultAsync(c => c.Id == id);
    }

    public Task UpdateStatus(CheckList checkList, Status status)
    {
        checkList.Status = status;
        _dbContext.Entry(checkList).State = EntityState.Modified;
        return _dbContext.SaveChangesAsync();
    }
}
