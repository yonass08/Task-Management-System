using AutoMapper;
using Moq;
using Shouldly;
using TaskManagementSystem.Application.Contracts.Persistence;
using TaskManagementSystem.Application.Exceptions;
using TaskManagementSystem.Application.Features.User.CQRS.Handlers.Commands;
using TaskManagementSystem.Application.Features.User.CQRS.Requests.Commands;
using TaskManagementSystem.Application.Features.User.DTO;
using TaskManagementSystem.Application.Profiles;
using TaskManagementSystem.Tests.Mocks;

namespace TaskManagementSystem.Tests.User.Commands;

public class DeleteUserCommandHandlerTests
{
    private readonly IMapper _mapper;
    
    private readonly Mock<IUnitOfWork> _mockUnitOfWork;

    private readonly DeleteUserCommandHandler _handler;

    public DeleteUserCommandHandlerTests()
    {
        _mockUnitOfWork = MockUnitOfWork.GetUnitOfWork();
        
        var mapperConfig = new MapperConfiguration(c => 
        {
            c.AddProfile<MappingProfile>();
        });

        _mapper = mapperConfig.CreateMapper();
        _handler = new DeleteUserCommandHandler(_mockUnitOfWork.Object, _mapper);


    }

    [Fact]
    public async Task ShouldDeleteUser_WhenIdExists()
    {

        DeleteUserDto deleteUserDto = new() { Id = 1};
        
        var result = await _handler.Handle(new DeleteUserCommand() {  deleteUserDto =  deleteUserDto}, CancellationToken.None);
        
        
        (await _mockUnitOfWork.Object.UserRepository.GetAll()).Count.ShouldBe(1);

    }
       

    [Fact]
    public async Task ShouldThrowException_WhenIdDoesNotExist()
    {
        
        DeleteUserDto deleteUserDto = new() { Id = 0 };
        
        await Should.ThrowAsync<ValidationException>(async () => 
            await _handler.Handle(new DeleteUserCommand() { deleteUserDto =  deleteUserDto}, CancellationToken.None)
        );

        var User = await _mockUnitOfWork.Object.UserRepository.GetAll();
        User.Count.ShouldBe(2);

    }

}
