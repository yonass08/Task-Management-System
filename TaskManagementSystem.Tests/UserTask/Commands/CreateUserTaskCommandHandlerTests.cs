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

namespace TaskManagementSystem.Tests.UserTask.Commands;

public class CreateUserTaskCommandHandlerTests
{
    private readonly IMapper _mapper;

    private readonly Mock<IUnitOfWork> _mockUnitOfWork;

    private readonly CreateUserTaskCommandHandler _handler;

    public CreateUserTaskCommandHandlerTests()
    {
        _mockUnitOfWork = MockUnitOfWork.GetUnitOfWork();
        
        var mapperConfig = new MapperConfiguration(c => 
        {
            c.AddProfile<MappingProfile>();
        });

        _mapper = mapperConfig.CreateMapper();
        var userService = MockUserService.GetUserServiceService().Object;

        _handler = new CreateUserTaskCommandHandler(_mockUnitOfWork.Object,
                                                    _mapper, 
                                                    userService,
                                                    MockAuthorizationService.GetAuthorizationService().Object);


    }

    [Fact]
    public async Task Valid_UserTask_Added()
    {
        var UserTaskDto = new CreateUserTaskDto
        {
            UserId="UserId",
            Title= "Do something",
            Description = "this is the first UserTask",
            StartDate=DateTime.Now,
            EndDate=DateTime.Now.AddDays(2)

        };

        var command = new CreateUserTaskCommand()
        {
            createUserTaskDto = UserTaskDto, 
            UserId = "UserId"
        };

        var result = await _handler.Handle(command, CancellationToken.None);

        var UserTask = await _mockUnitOfWork.Object.UserTaskRepository.GetAll();

        result.ShouldBeOfType<int>();

        UserTask.Count.ShouldBe(3);
    }

    [Fact]
    public async Task InValid_UserTask_Added()
    {
         var UserTaskDto = new CreateUserTaskDto
        {
            UserId="UserId",
            Description = "this is the first UserTask",
            StartDate=DateTime.Now,
            EndDate=DateTime.Now
        };

        var command = new CreateUserTaskCommand()
        {
             createUserTaskDto = UserTaskDto, 
             UserId = "UserId"
        };

        await Should.ThrowAsync<ValidationException>(async () => 
            await _handler.Handle(command, CancellationToken.None)
        );

        var UserTask = await _mockUnitOfWork.Object.UserTaskRepository.GetAll();
        UserTask.Count.ShouldBe(2);
        
    }
}

