using AutoMapper;
using TaskManagementSystem.Application.Contracts.Persistence;
using TaskManagementSystem.Application.Features.User.CQRS.Requests.Commands;
using TaskManagementSystem.Application.Features.User.DTO;
using TaskManagementSystem.Application.Profiles;
using TaskManagementSystem.Tests.Mocks;
using TaskManagementSystem.Application.Features.User.CQRS.Handlers.Commands;
using TaskManagementSystem.Application.Exceptions;
using Shouldly;
using Moq;

namespace TaskManagementSystem.Tests.User.Commands
{
     public class UpdateUserCommandHandlerTests
    {
        private readonly IMapper _mapper;
        private readonly Mock<IUnitOfWork> _mockUnitOfWork;
        private readonly UpdateUserDto _userDto;
        private readonly UpdateUserDto _invalidUserDto;

        private readonly UpdateUserCommandHandler _handler;

        public UpdateUserCommandHandlerTests()
        {
            _mockUnitOfWork = MockUnitOfWork.GetUnitOfWork();
            
            var mapperConfig = new MapperConfiguration(c => 
            {
                c.AddProfile<MappingProfile>();
            });

            _mapper = mapperConfig.CreateMapper();
            _handler = new UpdateUserCommandHandler(_mockUnitOfWork.Object, _mapper);

            _userDto = new UpdateUserDto
            {
                Id = 1,
                FullName = "John Doe",
            };

            _invalidUserDto = new UpdateUserDto
            {
                Id = 1,
                FullName = "",
            };
        }

        [Fact]
        public async Task ShouldUpdate_WhenValidRequestMade()
        {
            var result = await _handler.Handle(new UpdateUserCommand() { updateUserDto = _userDto }, CancellationToken.None);

            var User = await _mockUnitOfWork.Object.UserRepository.Get(_userDto.Id);

            User.FullName.ShouldBe(_userDto.FullName);

            var Users = await _mockUnitOfWork.Object.UserRepository.GetAll();

            Users.Count.ShouldBe(2);
        }

        [Fact]
        public async Task ShouldThrowError_WhenInvalidRequestMade()
        {
            await Should.ThrowAsync<ValidationException>(async () => 
                await _handler.Handle(new UpdateUserCommand() { updateUserDto = _invalidUserDto}, CancellationToken.None)
            );

            var User = await _mockUnitOfWork.Object.UserRepository.GetAll();
            User.Count.ShouldBe(2);
            
        }
    }
}
