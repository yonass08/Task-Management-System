// using AutoMapper;
// using Moq;
// using Shouldly;
// using TaskManagementSystem.Application.Contracts.Persistence;
// using TaskManagementSystem.Application.Exceptions;
// using TaskManagementSystem.Application.Features.UserTask.CQRS.Handlers.Commands;
// using TaskManagementSystem.Application.Features.UserTask.CQRS.Requests.Commands;
// using TaskManagementSystem.Application.Features.UserTask.DTO;
// using TaskManagementSystem.Application.Profiles;
// using TaskManagementSystem.Tests.Mocks;

// namespace TaskManagementSystem.Tests.UserTask.Commands;

// public class DeleteUserTaskCommandHandlerTests
// {
//     private readonly IMapper _mapper;
    
//     private readonly Mock<IUnitOfWork> _mockUnitOfWork;

//     private readonly DeleteUserTaskCommandHandler _handler;

//     public DeleteUserTaskCommandHandlerTests()
//     {
//         _mockUnitOfWork = MockUnitOfWork.GetUnitOfWork();
        
//         var mapperConfig = new MapperConfiguration(c => 
//         {
//             c.AddProfile<MappingProfile>();
//         });

//         _mapper = mapperConfig.CreateMapper();
//         _handler = new DeleteUserTaskCommandHandler(_mockUnitOfWork.Object, _mapper);


//     }

//     [Fact]
//     public async Task ShouldDeleteUserTask_WhenIdExists()
//     {

//         DeleteUserTaskDto deleteUserTaskDto = new() { Id = 1};
        
//         var result = await _handler.Handle(new DeleteUserTaskCommand() {  deleteUserTaskDto =  deleteUserTaskDto}, CancellationToken.None);
        
        
//         (await _mockUnitOfWork.Object.UserTaskRepository.GetAll()).Count.ShouldBe(1);

//     }
       

//     [Fact]
//     public async Task ShouldThrowException_WhenIdDoesNotExist()
//     {
        
//         DeleteUserTaskDto deleteUserTaskDto = new() { Id = 0 };
        
//         await Should.ThrowAsync<ValidationException>(async () => 
//             await _handler.Handle(new DeleteUserTaskCommand() { deleteUserTaskDto =  deleteUserTaskDto}, CancellationToken.None)
//         );

//         var UserTask = await _mockUnitOfWork.Object.UserTaskRepository.GetAll();
//         UserTask.Count.ShouldBe(2);

//     }

// }
