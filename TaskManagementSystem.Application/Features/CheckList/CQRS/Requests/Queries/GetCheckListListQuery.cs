using MediatR;
using TaskManagementSystem.Application.Features.CheckList.DTO;

namespace TaskManagementSystem.Application.Features.CheckList.CQRS.Requests.Queries;

public class GetCheckListListQuery: IRequest<List<GetCheckListListDto>>
{
}
