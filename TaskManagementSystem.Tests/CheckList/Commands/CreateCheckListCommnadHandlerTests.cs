using AutoMapper;
using TaskManagementSystem.Application.Contracts.Persistence;
using TaskManagementSystem.Application.Features.CheckList.CQRS.Requests.Commands;
using TaskManagementSystem.Application.Features.CheckList.DTO;
using TaskManagementSystem.Application.Profiles;
using TaskManagementSystem.Tests.Mocks;
using TaskManagementSystem.Application.Features.CheckList.CQRS.Handlers.Commands;
using TaskManagementSystem.Application.Exceptions;
using Shouldly;
using Moq;

namespace TaskManagementSystem.Tests.CheckList.Commands;

public class CreateCheckListCommandHandlerTests
{
    private readonly IMapper _mapper;
    private readonly Mock<IUnitOfWork> _mockUnitOfWork;
    private readonly CreateCheckListDto _CheckListDto;
    private readonly CreateCheckListDto _invalidCheckListDto;

    private readonly CreateCheckListCommandHandler _handler;

    public CreateCheckListCommandHandlerTests()
    {
        _mockUnitOfWork = MockUnitOfWork.GetUnitOfWork();
        
        var mapperConfig = new MapperConfiguration(c => 
        {
            c.AddProfile<MappingProfile>();
        });

        _mapper = mapperConfig.CreateMapper();
        _handler = new CreateCheckListCommandHandler(_mockUnitOfWork.Object, _mapper, MockAuthorizationService.GetAuthorizationService().Object);

        _CheckListDto = new CreateCheckListDto
        {
            UserTaskId=1,
            Title= "Do something",
            Description = "this is the first checklist"

        };

        _invalidCheckListDto = new CreateCheckListDto
        {
            UserTaskId=1,
            Title= "",
            Description = "this is the first checklist"
        };
    }

    [Fact]
    public async Task Valid_CheckList_Added()
    {
        var result = await _handler.Handle(new CreateCheckListCommand() { createCheckListDto = _CheckListDto, UserId = "efa06a55-d0cc-4e01-abbf-870f21d91441" }, CancellationToken.None);

        var CheckList = await _mockUnitOfWork.Object.CheckListRepository.GetAll();

        result.ShouldBeOfType<int>();

        CheckList.Count.ShouldBe(3);
    }

    [Fact]
    public async Task InValid_CheckList_Added()
    {

        await Should.ThrowAsync<ValidationException>(async () => 
            await _handler.Handle(new CreateCheckListCommand() { createCheckListDto = _invalidCheckListDto, UserId = "efa06a55-d0cc-4e01-abbf-870f21d91441"}, CancellationToken.None)
        );

        var CheckList = await _mockUnitOfWork.Object.CheckListRepository.GetAll();
        CheckList.Count.ShouldBe(2);
        
    }
}

