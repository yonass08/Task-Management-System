using MediatR;
using TaskManagementSystem.Application.Features.User.DTO;

namespace TaskManagementSystem.Application.Features.User.CQRS.Requests.Queries;

public class GetUserListQuery: IRequest<List<GetUserListDto>>
{
    
}
