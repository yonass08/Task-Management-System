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

public class GetCheckListListQueryHandlerTests
{
    private readonly IMapper _mapper;
    
    private readonly Mock<IUnitOfWork> _mockUnitOfWork;

    private readonly GetCheckListListQueryHandler _handler;

    public GetCheckListListQueryHandlerTests()
    {
        _mockUnitOfWork = MockUnitOfWork.GetUnitOfWork();
        
        var mapperConfig = new MapperConfiguration(c => 
        {
            c.AddProfile<MappingProfile>();
        });

        _mapper = mapperConfig.CreateMapper();
        _handler = new GetCheckListListQueryHandler(_mockUnitOfWork.Object, _mapper, MockAuthorizationService.GetAuthorizationService().Object);


    }

    [Fact]
    public async Task ShouldGetCheckListList()
    {

        var result = await _handler.Handle(new GetCheckListListQuery(){UserId = "admin"}, CancellationToken.None);
        result.ShouldNotBe(null);
    }
       

}
