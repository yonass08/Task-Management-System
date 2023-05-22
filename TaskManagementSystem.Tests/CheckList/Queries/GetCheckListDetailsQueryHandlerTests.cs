using AutoMapper;
using Moq;
using Shouldly;
using TaskManagementSystem.Application.Contracts.Persistence;
using TaskManagementSystem.Application.Exceptions;
using TaskManagementSystem.Application.Features.CheckList.CQRS.Handlers.Queries;
using TaskManagementSystem.Application.Features.CheckList.CQRS.Requests.Queries;
using TaskManagementSystem.Application.Profiles;
using TaskManagementSystem.Tests.Mocks;

namespace TaskManagementSystem.Tests.CheckList.Queries;

public class GetCheckListDetailQueryHandlerTests
{
    private readonly IMapper _mapper;
    
    private readonly Mock<IUnitOfWork> _mockUnitOfWork;

    private readonly GetCheckListDetailQueryHandler _handler;

    public GetCheckListDetailQueryHandlerTests()
    {
        _mockUnitOfWork = MockUnitOfWork.GetUnitOfWork();
        
        var mapperConfig = new MapperConfiguration(c => 
        {
            c.AddProfile<MappingProfile>();
        });

        _mapper = mapperConfig.CreateMapper();
        _handler = new GetCheckListDetailQueryHandler(_mockUnitOfWork.Object, _mapper, MockAuthorizationService.GetAuthorizationService().Object);


    }

    [Fact]
    public async Task ShouldGetCheckListDetail_WhenIdExists()
    {

        var result = await _handler.Handle(new GetCheckListDetailQuery() { Id = 1, UserId = "efa06a55-d0cc-4e01-abbf-870f21d91441"}, CancellationToken.None);
        result.ShouldNotBe(null);
    }
       

    [Fact]
    public async Task ShouldThrowException_WhenIdDoesNotExist()
    {
        await Should.ThrowAsync<NotFoundException>(async () => 
             await _handler.Handle(new GetCheckListDetailQuery() { Id = 0, UserId = "efa06a55-d0cc-4e01-abbf-870f21d91441"}, CancellationToken.None)
        );
    }

}
