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
    private readonly CreateUserTaskDto _UserTaskDto;
    private readonly CreateUserTaskDto _invalidUserTaskDto;

    private readonly CreateUserTaskCommandHandler _handler;

    public CreateUserTaskCommandHandlerTests()
    {
        _mockUnitOfWork = MockUnitOfWork.GetUnitOfWork();
        
        var mapperConfig = new MapperConfiguration(c => 
        {
            c.AddProfile<MappingProfile>();
        });

        _mapper = mapperConfig.CreateMapper();
        _handler = new CreateUserTaskCommandHandler(_mockUnitOfWork.Object, _mapper);

        _UserTaskDto = new CreateUserTaskDto
        {
            UserId=1,
            Title= "Do something",
            Description = "this is the first UserTask",
            StartDate=DateTime.Now,
            EndDate=DateTime.MaxValue

        };

        _invalidUserTaskDto = new CreateUserTaskDto
        {
            UserId=0,
            Title= "Do something",
            Description = "this is the first UserTask",
            StartDate=DateTime.Now,
            EndDate=DateTime.MaxValue
        };
    }

    [Fact]
    public async Task Valid_UserTask_Added()
    {
        var result = await _handler.Handle(new CreateUserTaskCommand() { createUserTaskDto = _UserTaskDto }, CancellationToken.None);

        var UserTask = await _mockUnitOfWork.Object.UserTaskRepository.GetAll();

        result.ShouldBeOfType<int>();

        UserTask.Count.ShouldBe(3);
    }

    [Fact]
    public async Task InValid_UserTask_Added()
    {

        await Should.ThrowAsync<ValidationException>(async () => 
            await _handler.Handle(new CreateUserTaskCommand() { createUserTaskDto = _invalidUserTaskDto}, CancellationToken.None)
        );

        var UserTask = await _mockUnitOfWork.Object.UserTaskRepository.GetAll();
        UserTask.Count.ShouldBe(2);
        
    }
}

