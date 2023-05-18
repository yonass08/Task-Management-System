namespace TaskManagementSystem.Application.Contracts.Persistence;

public interface IUnitOfWork : IDisposable
{
 
    IUserRepository UserRepository {get;}

    IUserTaskRepository UserTaskRepository {get;}

    ICheckListRepository CheckListRepository {get;}
    
    Task <int> Save();

}
