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
        private readonly UpdateCheckListDto _CheckListDto;
        private readonly UpdateCheckListDto _invalidCheckListDto;

        private readonly UpdateCheckListCommandHandler _handler;

        public UpdateCheckListCommandHandlerTests()
        {
            _mockUnitOfWork = MockUnitOfWork.GetUnitOfWork();
            
            var mapperConfig = new MapperConfiguration(c => 
            {
                c.AddProfile<MappingProfile>();
            });

            _mapper = mapperConfig.CreateMapper();
            _handler = new UpdateCheckListCommandHandler(_mockUnitOfWork.Object, _mapper);

            _CheckListDto = new UpdateCheckListDto
            {
                Id = 1,
                Title= "Do something",
                Description = "this is the first checklist"
            };

            _invalidCheckListDto = new UpdateCheckListDto
            {
                Id = 0,
                Title= "Do something",
                Description = "this is the first checklist"
            };
        }

        [Fact]
        public async Task ShouldUpdate_WhenValidRequestMade()
        {
            var result = await _handler.Handle(new UpdateCheckListCommand() { updateCheckListDto = _CheckListDto }, CancellationToken.None);

            var CheckList = await _mockUnitOfWork.Object.CheckListRepository.Get(_CheckListDto.Id);

            CheckList.Title.ShouldBe(_CheckListDto.Title);
            CheckList.Description.ShouldBe(_CheckListDto.Description);

            var CheckLists = await _mockUnitOfWork.Object.CheckListRepository.GetAll();

            CheckLists.Count.ShouldBe(2);
        }

        [Fact]
        public async Task ShouldThrowError_WhenInvalidRequestMade()
        {
            await Should.ThrowAsync<ValidationException>(async () => 
                await _handler.Handle(new UpdateCheckListCommand() { updateCheckListDto = _invalidCheckListDto}, CancellationToken.None)
            );

            var CheckList = await _mockUnitOfWork.Object.CheckListRepository.GetAll();
            CheckList.Count.ShouldBe(2);
            
        }
    }
}
