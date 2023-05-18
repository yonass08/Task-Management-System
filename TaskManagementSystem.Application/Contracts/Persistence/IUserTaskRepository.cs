using TaskManagementSystem.Domain;

namespace TaskManagementSystem.Application.Contracts.Persistence;

public interface IUserTaskRepository: IGenericRepository<UserTask>
{
    Task UpdateStatus(UserTask userTask, Status status);

}
