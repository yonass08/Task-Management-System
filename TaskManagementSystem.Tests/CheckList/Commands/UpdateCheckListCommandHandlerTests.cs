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

namespace TaskManagementSystem.Tests.CheckList.Commands
{
     public class UpdateCheckListCommandHandlerTests
    {
        private readonly IMapper _mapper;
        private readonly Mock<IUnitOfWork> _mockUnitOfWork;

        private readonly UpdateCheckListCommandHandler _handler;

        public UpdateCheckListCommandHandlerTests()
        {
            _mockUnitOfWork = MockUnitOfWork.GetUnitOfWork();
            
            var mapperConfig = new MapperConfiguration(c => 
            {
                c.AddProfile<MappingProfile>();
            });

            _mapper = mapperConfig.CreateMapper();
            _handler = new UpdateCheckListCommandHandler(_mockUnitOfWork.Object, _mapper, MockAuthorizationService.GetAuthorizationService().Object);

        }

        [Fact]
        public async Task ShouldUpdate_WhenValidRequestMade()
        {
            var command = new UpdateCheckListCommand()
            {
                UserId = "UserId",
                updateCheckListDto = new UpdateCheckListDto
                {
                    Id = 1,
                    Title= "Do something",
                    Description = "this is the first checklist"
                }
            };

            var result = await _handler.Handle(command, CancellationToken.None);

            var CheckList = await _mockUnitOfWork.Object.CheckListRepository.Get(1);

            CheckList.Title.ShouldBe("Do something");
            CheckList.Description.ShouldBe("this is the first checklist");

            var CheckLists = await _mockUnitOfWork.Object.CheckListRepository.GetAll();

            CheckLists.Count.ShouldBe(2);
        }

        [Fact]
        public async Task ShouldThrowError_WhenInvalidRequestMade()
        {
            var command = new UpdateCheckListCommand()
            {
                UserId = "UserId",
                updateCheckListDto = new UpdateCheckListDto
                {
                    Id = 0,
                    Title= "Do something",
                    Description = "this is the first checklist"
                }
            };

            await Should.ThrowAsync<ValidationException>(async () => 
                await _handler.Handle(command, CancellationToken.None)
            );

            var CheckList = await _mockUnitOfWork.Object.CheckListRepository.GetAll();
            CheckList.Count.ShouldBe(2);
            
        }
    }
}
