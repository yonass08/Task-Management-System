using MediatR;
using TaskManagementSystem.Application.Features.User.DTO;

namespace TaskManagementSystem.Application.Features.User.CQRS.Requests.Queries;

public class GetUserDetailQuery: IRequest<GetUserDetailDto>
{
    public int Id {get; set;}
    
}
