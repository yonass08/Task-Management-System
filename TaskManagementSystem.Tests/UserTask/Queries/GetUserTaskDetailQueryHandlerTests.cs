using AutoMapper;
using Moq;
using Shouldly;
using TaskManagementSystem.Application.Contracts.Persistence;
using TaskManagementSystem.Application.Exceptions;
using TaskManagementSystem.Application.Features.UserTask.CQRS.Handlers.Queries;
using TaskManagementSystem.Application.Features.UserTask.CQRS.Requests.Queries;
using TaskManagementSystem.Application.Profiles;
using TaskManagementSystem.Tests.Mocks;

namespace TaskManagementSystem.Tests.UserTask.Queries;

public class GetUserTaskDetailQueryHandlerTests
{
    private readonly IMapper _mapper;
    
    private readonly Mock<IUnitOfWork> _mockUnitOfWork;

    private readonly GetUserTaskDetailQueryHandler _handler;

    public GetUserTaskDetailQueryHandlerTests()
    {
        _mockUnitOfWork = MockUnitOfWork.GetUnitOfWork();
        
        var mapperConfig = new MapperConfiguration(c => 
        {
            c.AddProfile<MappingProfile>();
        });

        _mapper = mapperConfig.CreateMapper();
        _handler = new GetUserTaskDetailQueryHandler(_mockUnitOfWork.Object, _mapper, MockAuthorizationService.GetAuthorizationService().Object);


    }

    [Fact]
    public async Task ShouldGetUserTaskDetail_WhenIdExists()
    {
        var Query = new GetUserTaskDetailQuery()
        {
             Id = 1,
             UserId = "UserId"
        };

        var result = await _handler.Handle(Query, CancellationToken.None);
        result.ShouldNotBe(null);
    }
       

    [Fact]
    public async Task ShouldThrowException_WhenIdDoesNotExist()
    {
        var Query = new GetUserTaskDetailQuery()
        {
             Id = 0,
             UserId = "UserId"
        };

        await Should.ThrowAsync<NotFoundException>(async () => 
             await _handler.Handle(Query, CancellationToken.None)
        );
    }

}
