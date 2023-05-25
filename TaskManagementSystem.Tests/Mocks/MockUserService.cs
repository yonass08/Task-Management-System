using Moq;
using TaskManagementSystem.Application.Contracts.Identity;
using TaskManagementSystem.Application.Models.Identity;

namespace TaskManagementSystem.Tests.Mocks;

public class MockUserService
{
    public static Mock<IUserService> GetUserServiceService()
    {

        var Users = new List<User>()
        {
            new User
            {
                Id = "UserId",
                Email = "Admin@TMS.com",
                FullName = "debebe",
                UserName = "Admin@TMS.com",
                Roles = new List<string>(){"Admin"}
            },

            new User
            {
                Id = "AdminId",
                Email = "User@TMS.com",
                FullName = "kebede",
                UserName = "User@TMS.com",
                Roles = new List<string>(){"User"}
            }
        };


        var mockService = new Mock<IUserService>();

        mockService.Setup(r => r.GetUsers())
            .ReturnsAsync(Users);
        

        mockService.Setup(r => r.GetUser(It.IsAny<string>()))
            .ReturnsAsync((string Id) =>
            {
                return Users.Where(u => u.Id == Id).FirstOrDefault();
            });


        mockService.Setup(r => r.Exists(It.IsAny<string>()))
            .ReturnsAsync((string Id) =>
            {
                var user = Users.Where(u => u.Id == Id).FirstOrDefault();
                return user != null;
            });

        return mockService;
    }
}
