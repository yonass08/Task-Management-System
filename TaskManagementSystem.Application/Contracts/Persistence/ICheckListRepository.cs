using TaskManagementSystem.Domain;

namespace TaskManagementSystem.Application.Contracts.Persistence;

public interface ICheckListRepository: IGenericRepository<CheckList>
{
    Task UpdateStatus(CheckList checkList, Status status);

}
