using TaskManagementSystem.Application.Contracts.Persistence;
namespace TaskManagementSystem.Persistence.Repositories;

public class UnitOfWork : IUnitOfWork
{

    private readonly TaskManagementSystemDbContext _context;
    private IUserRepository _userRepository;
    private IUserTaskRepository _userTaskRepository;
    private ICheckListRepository _checkListRepository;


    public UnitOfWork(TaskManagementSystemDbContext context)
    {
        _context = context;
    }

    IUserRepository IUnitOfWork.UserRepository 
    { 
        get 
        {
            if (_userRepository == null)
                _userRepository = new UserRepository(_context);
            return _userRepository;
        } 
    }

    IUserTaskRepository IUnitOfWork.UserTaskRepository 
    { 
        get 
        {
            if (_userTaskRepository == null)
                _userTaskRepository = new UserTaskRepository(_context);
            return _userTaskRepository;
        } 
    }

    ICheckListRepository IUnitOfWork.CheckListRepository 
    { 
        get 
        {
            if (_checkListRepository == null)
                _checkListRepository = new CheckListRepository(_context);
            return _checkListRepository;
        } 
    }

    public void Dispose()
    {
        _context.Dispose();
        GC.SuppressFinalize(this);
    }
    public async Task<int> Save()
    {
        return await _context.SaveChangesAsync();
    }
}

