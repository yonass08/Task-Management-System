using MediatR;
using TaskManagementSystem.Application.Features.User.DTO;

namespace TaskManagementSystem.Application.Features.User.CQRS.Requests.Commands;


public class DeleteUserCommand : IRequest
{
    public DeleteUserDto deleteUserDto {get; set;}

}