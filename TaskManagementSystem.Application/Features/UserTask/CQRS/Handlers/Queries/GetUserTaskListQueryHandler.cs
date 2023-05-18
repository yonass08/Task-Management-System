
using AutoMapper;
using MediatR;
using TaskManagementSystem.Application.Contracts.Persistence;
using TaskManagementSystem.Application.Features.UserTask.CQRS.Requests.Queries;
using TaskManagementSystem.Application.Features.UserTask.DTO;

namespace TaskManagementSystem.Application.Features.User.CQRS.Handlers.Queries;

public class GetUserTaskListQueryHandler : IRequestHandler<GetUserTaskListQuery, List<GetUserTaskListDto>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public GetUserTaskListQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<List<GetUserTaskListDto>> Handle(GetUserTaskListQuery request, CancellationToken cancellationToken)
    {
        var userTasks = await _unitOfWork.UserTaskRepository.GetAll();
        var userTaskDtos = _mapper.Map<List<GetUserTaskListDto>>(userTasks);
        return userTaskDtos;
    }
}



