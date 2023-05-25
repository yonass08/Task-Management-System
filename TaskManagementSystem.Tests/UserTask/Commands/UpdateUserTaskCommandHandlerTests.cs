using AutoMapper;
using TaskManagementSystem.Application.Contracts.Persistence;
using TaskManagementSystem.Application.Features.UserTask.CQRS.Requests.Commands;
using TaskManagementSystem.Application.Features.UserTask.DTO;
using TaskManagementSystem.Application.Profiles;
using TaskManagementSystem.Tests.Mocks;
using TaskManagementSystem.Application.Features.UserTask.CQRS.Handlers.Commands;
using TaskManagementSystem.Application.Exceptions;
using Shouldly;
using Moq;

namespace TaskManagementSystem.Tests.UserTask.Commands
{
     public class UpdateUserTaskCommandHandlerTests
    {
        private readonly IMapper _mapper;
        private readonly Mock<IUnitOfWork> _mockUnitOfWork;
        private readonly UpdateUserTaskCommandHandler _handler;

        public UpdateUserTaskCommandHandlerTests()
        {
            _mockUnitOfWork = MockUnitOfWork.GetUnitOfWork();
            
            var mapperConfig = new MapperConfiguration(c => 
            {
                c.AddProfile<MappingProfile>();
            });

            _mapper = mapperConfig.CreateMapper();
            _handler = new UpdateUserTaskCommandHandler(_mockUnitOfWork.Object, _mapper, MockAuthorizationService.GetAuthorizationService().Object);

        }

        [Fact]
        public async Task ShouldUpdate_WhenValidRequestMade()
        {
            var UserTaskDto = new UpdateUserTaskDto
            {
                Id = 1,
                Title= "Do something",
                Description = "this is the first UserTask",
                StartDate=DateTime.Now,
                EndDate=DateTime.MaxValue
            };

            var command = new UpdateUserTaskCommand()
            {
                updateUserTaskDto = UserTaskDto, 
                UserId = "UserId"
            };

            var result = await _handler.Handle(command, CancellationToken.None);

            var UserTask = await _mockUnitOfWork.Object.UserTaskRepository.Get(UserTaskDto.Id);

            UserTask.Title.ShouldBe(UserTaskDto.Title);
            UserTask.Description.ShouldBe(UserTaskDto.Description);
            UserTask.StartDate.ShouldBe(UserTaskDto.StartDate);
            UserTask.EndDate.ShouldBe(UserTaskDto.EndDate);

            var UserTasks = await _mockUnitOfWork.Object.UserTaskRepository.GetAll();

            UserTasks.Count.ShouldBe(2);
        }

        [Fact]
        public async Task ShouldThrowError_WhenInvalidRequestMade()
        {
            var UserTaskDto = new UpdateUserTaskDto
            {
                Id = 0,
                Title= "Do something",
                Description = "this is the first UserTask",
                StartDate=DateTime.Now,
                EndDate=DateTime.Now
            };

            var command = new UpdateUserTaskCommand()
            {
                updateUserTaskDto = UserTaskDto, 
                UserId = "UserId"
            };

            await Should.ThrowAsync<ValidationException>(async () => 
                await _handler.Handle(command, CancellationToken.None)
            );

            var UserTask = await _mockUnitOfWork.Object.UserTaskRepository.GetAll();
            UserTask.Count.ShouldBe(2);
            
        }
    }
}
