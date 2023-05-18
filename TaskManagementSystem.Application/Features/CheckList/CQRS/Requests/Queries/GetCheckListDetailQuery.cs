using MediatR;
using TaskManagementSystem.Application.Features.CheckList.DTO;

namespace TaskManagementSystem.Application.Features.CheckList.CQRS.Requests.Queries;

public class GetCheckListDetailQuery: IRequest<GetCheckListDetailDto>
{
    public int Id {get; set;}
}
