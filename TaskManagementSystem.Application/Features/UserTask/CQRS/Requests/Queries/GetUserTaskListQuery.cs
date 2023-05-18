using MediatR;
using TaskManagementSystem.Application.Features.UserTask.DTO;

namespace TaskManagementSystem.Application.Features.UserTask.CQRS.Requests.Queries;

public class GetUserTaskListQuery: IRequest<List<GetUserTaskListDto>>
{
}
