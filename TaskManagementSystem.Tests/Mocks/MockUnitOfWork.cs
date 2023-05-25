using TaskManagementSystem.Application.Contracts.Persistence;
using Moq;

namespace TaskManagementSystem.Tests.Mocks
{
    public static class MockUnitOfWork
    {
        public static Mock<IUnitOfWork> GetUnitOfWork()
        {
            var mockUnitOfWork = new Mock<IUnitOfWork>();
            var mockUserTaskRepo = MockUserTaskRepository.GetUserTaskRepository();
            var mockCheckListRepo = MockCheckListRepository.GetCheckListRepository();
            
            mockUnitOfWork.Setup(r => r.UserTaskRepository).Returns(mockUserTaskRepo.Object);
            mockUnitOfWork.Setup(r=>r.CheckListRepository).Returns(mockCheckListRepo.Object);

            mockUnitOfWork.Setup(r => r.Save()).ReturnsAsync(1);
            return mockUnitOfWork;
        }
    }
}

