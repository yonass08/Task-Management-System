// using AutoMapper;
// using Moq;
// using Shouldly;
// using TaskManagementSystem.Application.Contracts.Persistence;
// using TaskManagementSystem.Application.Exceptions;
// using TaskManagementSystem.Application.Features.UserTask.CQRS.Handlers.Queries;
// using TaskManagementSystem.Application.Features.UserTask.CQRS.Requests.Queries;
// using TaskManagementSystem.Application.Profiles;
// using TaskManagementSystem.Tests.Mocks;

// namespace TaskManagementSystem.Tests.UserTask.Queries;

// public class GetUserTaskListQueryHandlerTests
// {
//     private readonly IMapper _mapper;
    
//     private readonly Mock<IUnitOfWork> _mockUnitOfWork;

//     private readonly GetUserTaskListQueryHandler _handler;

//     public GetUserTaskListQueryHandlerTests()
//     {
//         _mockUnitOfWork = MockUnitOfWork.GetUnitOfWork();
        
//         var mapperConfig = new MapperConfiguration(c => 
//         {
//             c.AddProfile<MappingProfile>();
//         });

//         _mapper = mapperConfig.CreateMapper();
//         _handler = new GetUserTaskListQueryHandler(_mockUnitOfWork.Object, _mapper);


//     }

//     [Fact]
//     public async Task ShouldGetUserTaskList()
//     {

//         var result = await _handler.Handle(new GetUserTaskListQuery(), CancellationToken.None);
//         result.ShouldNotBe(null);
//     }
       

// }
