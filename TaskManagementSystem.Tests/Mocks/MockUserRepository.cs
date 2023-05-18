using TaskManagementSystem.Application.Contracts.Persistence;
using TaskManagementSystem.Domain;
using Moq;

namespace TaskManagementSystem.Tests.Mocks;

public class MockUserRepository
{

     public static Mock<IUserRepository> GetUserRepository()
    {
        var Users = new List<Domain.User>
        {
            new ()
            {
                Id=1,
                FullName="abebe",
                Email = "abebe@gmail.com",
            },
            
            new ()
            {
                Id=2,
                FullName="kebede",
                Email = "kebede@gmail.com",
            }
        };

        var mockRepo = new Mock<IUserRepository>();

        mockRepo.Setup(r => r.GetAll()).ReturnsAsync(Users);
        
        mockRepo.Setup(r => r.Add(It.IsAny<Domain.User>())).ReturnsAsync((Domain.User User) =>
        {
            User.Id = Users.Count() + 1;
            Users.Add(User);
            return User; 
        });

        mockRepo.Setup(r => r.Update(It.IsAny<Domain.User>())).Callback((Domain.User User) =>
        {
            var newUsers = Users.Where((r) => r.Id != User.Id);
            Users = newUsers.ToList();
            Users.Add(User);
        });
        
        mockRepo.Setup(r => r.Delete(It.IsAny<Domain.User>())).Callback((Domain.User User) =>
        
        {
            if (Users.Exists(b => b.Id == User.Id))
                Users.Remove(Users.Find(b => b.Id == User.Id)!);
        });

         mockRepo.Setup(r => r.Exists(It.IsAny<int>())).ReturnsAsync((int id) =>
        {
            var User = Users.FirstOrDefault((r) => r.Id == id);
            return User != null;
        });

        
        mockRepo.Setup(r => r.Get(It.IsAny<int>()))!.ReturnsAsync((int id) =>
        {
            return Users.FirstOrDefault((r) => r.Id == id);
        });

        mockRepo.Setup(r => r.GetByEmail(It.IsAny<string>()))!.ReturnsAsync((string Email) =>
        {
            return Users.FirstOrDefault((r) => r.Email == Email);
        });

        return mockRepo;
    }
}