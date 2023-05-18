using AutoMapper;
using Moq;
using Shouldly;
using TaskManagementSystem.Application.Contracts.Persistence;
using TaskManagementSystem.Application.Exceptions;
using TaskManagementSystem.Application.Features.User.CQRS.Handlers.Queries;
using TaskManagementSystem.Application.Features.User.CQRS.Requests.Queries;
using TaskManagementSystem.Application.Profiles;
using TaskManagementSystem.Tests.Mocks;

namespace TaskManagementSystem.Tests.User.Queries;

public class GetUserListQueryHandlerTests
{
    private readonly IMapper _mapper;
    
    private readonly Mock<IUnitOfWork> _mockUnitOfWork;

    private readonly GetUserListQueryHandler _handler;

    public GetUserListQueryHandlerTests()
    {
        _mockUnitOfWork = MockUnitOfWork.GetUnitOfWork();
        
        var mapperConfig = new MapperConfiguration(c => 
        {
            c.AddProfile<MappingProfile>();
        });

        _mapper = mapperConfig.CreateMapper();
        _handler = new GetUserListQueryHandler(_mockUnitOfWork.Object, _mapper);


    }

    [Fact]
    public async Task ShouldGetUserList()
    {

        var result = await _handler.Handle(new GetUserListQuery(), CancellationToken.None);
        result.ShouldNotBe(null);
    }
       

}
