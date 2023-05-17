namespace TaskManagementSystem.Application.Contracts.Persistence;

public interface IUnitOfWork : IDisposable
{
 
    IUserRepository UserRepository {get; set;}

    IUserTaskRepository UserTaskRepository {get; set;}

    ICheckListRepository CheckListRepository {get; set;}
    
    Task <int> Save();

}
