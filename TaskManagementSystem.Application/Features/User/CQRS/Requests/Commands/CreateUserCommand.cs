using MediatR;
using TaskManagementSystem.Application.Features.User.DTO;

namespace TaskManagementSystem.Application.Features.User.CQRS.Requests.Commands;

public class CreateUserCommand : IRequest<int>
{
    public CreateUserDto createUserDto {get; set;}

}

