namespace TaskManagementSystem.Application.Contracts.Persistence;

public interface IUnitOfWork : IDisposable
{
 
    IUserTaskRepository UserTaskRepository {get;}

    ICheckListRepository CheckListRepository {get;}
    
    Task <int> Save();

}
