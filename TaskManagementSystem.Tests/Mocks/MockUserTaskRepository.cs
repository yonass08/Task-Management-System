using TaskManagementSystem.Application.Contracts.Persistence;
using TaskManagementSystem.Domain;
using Moq;

namespace TaskManagementSystem.Tests.Mocks;

public class MockUserTaskRepository
{

    public static Mock<IUserTaskRepository> GetUserTaskRepository()
    {
        var UserTasks = new List<TaskManagementSystem.Domain.UserTask>
        {
            new ()
            {
                Id=1,
                UserId=1,
                Title= "Do something",
                CreatedAt=DateTime.Now,
                LastUpdated=DateTime.Now,
                Description = "this is the first UserTask",
                StartDate=DateTime.Now,
                EndDate=DateTime.MaxValue
            },
            
            new ()
            {
                Id=2,
                UserId=1,
                Title= "Do something",
                CreatedAt=DateTime.Now,
                LastUpdated=DateTime.Now,
                Description = "this is the first UserTask",
                StartDate=DateTime.Now,
                EndDate=DateTime.MaxValue
            }
        };

        var mockRepo = new Mock<IUserTaskRepository>();

        mockRepo.Setup(r => r.GetAll()).ReturnsAsync(UserTasks);
        
        mockRepo.Setup(r => r.Add(It.IsAny<Domain.UserTask>())).ReturnsAsync((Domain.UserTask UserTask) =>
        {
            UserTask.Id = UserTasks.Count() + 1;
            UserTasks.Add(UserTask);
            return UserTask; 
        });

        mockRepo.Setup(r => r.Update(It.IsAny<Domain.UserTask>())).Callback((Domain.UserTask UserTask) =>
        {
            var newUserTasks = UserTasks.Where((r) => r.Id != UserTask.Id);
            UserTasks = newUserTasks.ToList();
            UserTasks.Add(UserTask);
        });
        
        mockRepo.Setup(r => r.Delete(It.IsAny<Domain.UserTask>())).Callback((Domain.UserTask UserTask) =>
        
        {
            if (UserTasks.Exists(b => b.Id == UserTask.Id))
                UserTasks.Remove(UserTasks.Find(b => b.Id == UserTask.Id)!);
        });

         mockRepo.Setup(r => r.Exists(It.IsAny<int>())).ReturnsAsync((int id) =>
        {
            var UserTask = UserTasks.FirstOrDefault((r) => r.Id == id);
            return UserTask != null;
        });

        
        mockRepo.Setup(r => r.Get(It.IsAny<int>()))!.ReturnsAsync((int id) =>
        {
            return UserTasks.FirstOrDefault((r) => r.Id == id);
        });

        return mockRepo;
    }
}