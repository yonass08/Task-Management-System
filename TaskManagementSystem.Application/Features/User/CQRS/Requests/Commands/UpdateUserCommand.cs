using MediatR;
using TaskManagementSystem.Application.Features.User.DTO;

namespace TaskManagementSystem.Application.Features.User.CQRS.Requests.Commands;

public class UpdateUserCommand : IRequest
{
    public UpdateUserDto updateUserDto {get; set;}

}