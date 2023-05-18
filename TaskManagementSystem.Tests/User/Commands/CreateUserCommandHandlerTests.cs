using AutoMapper;
using TaskManagementSystem.Application.Contracts.Persistence;
using TaskManagementSystem.Application.Features.User.CQRS.Requests.Commands;
using TaskManagementSystem.Application.Features.User.DTO;
using TaskManagementSystem.Application.Profiles;
using TaskManagementSystem.Tests.Mocks;
using TaskManagementSystem.Application.Features.User.CQRS.Handlers.Commands;
using TaskManagementSystem.Application.Exceptions;
using Shouldly;
using Moq;

namespace TaskManagementSystem.Tests.User.Commands;

public class CreateUserCommandHandlerTests
{
    private readonly IMapper _mapper;
    private readonly Mock<IUnitOfWork> _mockUnitOfWork;
    private readonly CreateUserDto _userDto;
    private readonly CreateUserDto _invalidUserDto;

    private readonly CreateUserCommandHandler _handler;

    public CreateUserCommandHandlerTests()
    {
        _mockUnitOfWork = MockUnitOfWork.GetUnitOfWork();
        
        var mapperConfig = new MapperConfiguration(c => 
        {
            c.AddProfile<MappingProfile>();
        });

        _mapper = mapperConfig.CreateMapper();
        _handler = new CreateUserCommandHandler(_mockUnitOfWork.Object, _mapper);

        _userDto = new CreateUserDto
        {
            FullName = "John Doe",
            Email = "johndoe@example.com",

        };

        _invalidUserDto = new CreateUserDto
        {
            FullName = "",
            Email = "johndoeexample.com",
        };
    }

    [Fact]
    public async Task Valid_User_Added()
    {
        var result = await _handler.Handle(new CreateUserCommand() { createUserDto = _userDto }, CancellationToken.None);

        var User = await _mockUnitOfWork.Object.UserRepository.GetAll();

        result.ShouldBeOfType<int>();

        User.Count.ShouldBe(3);
    }

    [Fact]
    public async Task InValid_User_Added()
    {

        await Should.ThrowAsync<ValidationException>(async () => 
            await _handler.Handle(new CreateUserCommand() { createUserDto = _invalidUserDto}, CancellationToken.None)
        );

        var User = await _mockUnitOfWork.Object.UserRepository.GetAll();
        User.Count.ShouldBe(2);
        
    }
}

