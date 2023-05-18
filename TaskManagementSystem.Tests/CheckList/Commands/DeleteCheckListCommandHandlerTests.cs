using AutoMapper;
using Moq;
using Shouldly;
using TaskManagementSystem.Application.Contracts.Persistence;
using TaskManagementSystem.Application.Exceptions;
using TaskManagementSystem.Application.Features.CheckList.CQRS.Handlers.Commands;
using TaskManagementSystem.Application.Features.CheckList.CQRS.Requests.Commands;
using TaskManagementSystem.Application.Features.CheckList.DTO;
using TaskManagementSystem.Application.Profiles;
using TaskManagementSystem.Tests.Mocks;

namespace TaskManagementSystem.Tests.CheckList.Commands;

public class DeleteCheckListCommandHandlerTests
{
    private readonly IMapper _mapper;
    
    private readonly Mock<IUnitOfWork> _mockUnitOfWork;

    private readonly DeleteCheckListCommandHandler _handler;

    public DeleteCheckListCommandHandlerTests()
    {
        _mockUnitOfWork = MockUnitOfWork.GetUnitOfWork();
        
        var mapperConfig = new MapperConfiguration(c => 
        {
            c.AddProfile<MappingProfile>();
        });

        _mapper = mapperConfig.CreateMapper();
        _handler = new DeleteCheckListCommandHandler(_mockUnitOfWork.Object, _mapper);


    }

    [Fact]
    public async Task ShouldDeleteCheckList_WhenIdExists()
    {

        DeleteCheckListDto deleteCheckListDto = new() { Id = 1};
        
        var result = await _handler.Handle(new DeleteCheckListCommand() {  deleteCheckListDto =  deleteCheckListDto}, CancellationToken.None);
        
        
        (await _mockUnitOfWork.Object.CheckListRepository.GetAll()).Count.ShouldBe(1);

    }
       

    [Fact]
    public async Task ShouldThrowException_WhenIdDoesNotExist()
    {
        
        DeleteCheckListDto deleteCheckListDto = new() { Id = 0 };
        
        await Should.ThrowAsync<ValidationException>(async () => 
            await _handler.Handle(new DeleteCheckListCommand() { deleteCheckListDto =  deleteCheckListDto}, CancellationToken.None)
        );

        var CheckList = await _mockUnitOfWork.Object.CheckListRepository.GetAll();
        CheckList.Count.ShouldBe(2);

    }

}
