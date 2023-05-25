using Moq;
using TaskManagementSystem.Application.Contracts.Identity;
using TaskManagementSystem.Domain;

namespace TaskManagementSystem.Tests.Mocks;

public class MockAuthorizationService
{

    public static Mock<IAuthorizationService> GetAuthorizationService()
    {

        var UserTasks = new List<Domain.UserTask>()
        {
            new Domain.UserTask
            {
                Id = 1,
                UserId = "UserId",
                Title = "Do something",
                CreatedAt = DateTime.Now,
                LastUpdated = DateTime.Now,
                Description = "this is the first UserTask",
                StartDate = DateTime.Now,
                EndDate = DateTime.MaxValue
            },

            new Domain.UserTask
            {
                Id = 2,
                UserId = "UserId",
                Title = "Do something",
                CreatedAt = DateTime.Now,
                LastUpdated = DateTime.Now,
                Description = "this is the first UserTask",
                StartDate = DateTime.Now,
                EndDate = DateTime.MaxValue
            }
        };

        var CheckLists = new List<TaskManagementSystem.Domain.CheckList>
        {
            new ()
            {
                Id=1,
                UserTaskId=1,
                UserTask = UserTasks[0],
                Title= "Do something",
                CreatedAt=DateTime.Now,
                LastUpdated=DateTime.Now,
                Description = "this is the first checklist"
            },
            
            new ()
            {
                Id=2,
                UserTaskId=1,
                UserTask = UserTasks[0],
                Title= "Do something",
                CreatedAt=DateTime.Now,
                LastUpdated=DateTime.Now,
                Description = "this is the second checklist"
            }
        };

        var mockService = new Mock<IAuthorizationService>();

        mockService.Setup(r => r.UserTaskBelongsToUser(It.IsAny<Domain.UserTask>(), It.IsAny<string>()))
            .ReturnsAsync((Domain.UserTask t, string u) => 
            {
                return t.UserId == u;
            });
        

        mockService.Setup(r => r.UserCanCreatTask(It.IsAny<string>(), It.IsAny<string>()))
            .ReturnsAsync((string ctor, string cted) =>
            {
                return ctor == cted;
            });


        mockService.Setup(r => r.CheckListBelongsToUser(It.IsAny<Domain.CheckList>(), It.IsAny<string>()))
            .ReturnsAsync((Domain.CheckList c, string id) =>
            {
                return c.UserTask.UserId == id;
            });

      
        mockService.Setup(r => r.IsAdmin(It.IsAny<string>()))
            .ReturnsAsync((string userId) => 
            {
                if(userId == "Admin")
                    return true;
                return false;
            });


        return mockService;
    }
}