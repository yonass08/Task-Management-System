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

public class GetUserDetailQueryHandlerTests
{
    private readonly IMapper _mapper;
    
    private readonly Mock<IUnitOfWork> _mockUnitOfWork;

    private readonly GetUserDetailQueryHandler _handler;

    public GetUserDetailQueryHandlerTests()
    {
        _mockUnitOfWork = MockUnitOfWork.GetUnitOfWork();
        
        var mapperConfig = new MapperConfiguration(c => 
        {
            c.AddProfile<MappingProfile>();
        });

        _mapper = mapperConfig.CreateMapper();
        _handler = new GetUserDetailQueryHandler(_mockUnitOfWork.Object, _mapper);


    }

    [Fact]
    public async Task ShouldGetUserDetail_WhenIdExists()
    {

        var result = await _handler.Handle(new GetUserDetailQuery() { Id = 1}, CancellationToken.None);
        result.ShouldNotBe(null);
    }
       

    [Fact]
    public async Task ShouldThrowException_WhenIdDoesNotExist()
    {
        await Should.ThrowAsync<NotFoundException>(async () => 
             await _handler.Handle(new GetUserDetailQuery() { Id = 0}, CancellationToken.None)
        );
    }

}
