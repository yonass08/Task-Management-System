using TaskManagementSystem.Application.Contracts.Persistence;
using TaskManagementSystem.Domain;
using Moq;

namespace TaskManagementSystem.Tests.Mocks;

public class MockCheckListRepository
{

     public static Mock<ICheckListRepository> GetCheckListRepository()
    {
        var CheckLists = new List<TaskManagementSystem.Domain.CheckList>
        {
            new ()
            {
                Id=1,
                UserTaskId=1,
                Title= "Do something",
                CreatedAt=DateTime.Now,
                LastUpdated=DateTime.Now,
                Description = "this is the first checklist"
            },
            
            new ()
            {
                Id=2,
                UserTaskId=1,
                Title= "Do something",
                CreatedAt=DateTime.Now,
                LastUpdated=DateTime.Now,
                Description = "this is the second checklist"
            }
        };

        var mockRepo = new Mock<ICheckListRepository>();

        mockRepo.Setup(r => r.GetAll()).ReturnsAsync(CheckLists);
        
        mockRepo.Setup(r => r.Add(It.IsAny<CheckList>())).ReturnsAsync((CheckList CheckList) =>
        {
            CheckList.Id = CheckLists.Count() + 1;
            CheckLists.Add(CheckList);
            return CheckList; 
        });

        mockRepo.Setup(r => r.Update(It.IsAny<CheckList>())).Callback((CheckList CheckList) =>
        {
            var newCheckLists = CheckLists.Where((r) => r.Id != CheckList.Id);
            CheckLists = newCheckLists.ToList();
            CheckLists.Add(CheckList);
        });
        
        mockRepo.Setup(r => r.Delete(It.IsAny<CheckList>())).Callback((CheckList CheckList) =>
        
        {
            if (CheckLists.Exists(b => b.Id == CheckList.Id))
                CheckLists.Remove(CheckLists.Find(b => b.Id == CheckList.Id)!);
        });

         mockRepo.Setup(r => r.Exists(It.IsAny<int>())).ReturnsAsync((int id) =>
        {
            var CheckList = CheckLists.FirstOrDefault((r) => r.Id == id);
            return CheckList != null;
        });

        
        mockRepo.Setup(r => r.Get(It.IsAny<int>()))!.ReturnsAsync((int id) =>
        {
            return CheckLists.FirstOrDefault((r) => r.Id == id);
        });

        return mockRepo;
    }
}