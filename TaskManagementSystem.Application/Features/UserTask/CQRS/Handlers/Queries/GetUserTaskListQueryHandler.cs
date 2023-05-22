
using AutoMapper;
using MediatR;
using TaskManagementSystem.Application.Contracts.Identity;
using TaskManagementSystem.Application.Contracts.Persistence;
using TaskManagementSystem.Application.Exceptions;
using TaskManagementSystem.Application.Features.UserTask.CQRS.Requests.Queries;
using TaskManagementSystem.Application.Features.UserTask.DTO;

namespace TaskManagementSystem.Application.Features.UserTask.CQRS.Handlers.Queries;

public class GetUserTaskListQueryHandler : IRequestHandler<GetUserTaskListQuery, List<GetUserTaskListDto>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    private readonly IAuthorizationService _authService;

    public GetUserTaskListQueryHandler(IUnitOfWork unitOfWork, IMapper mapper, IAuthorizationService authService)
    {
        _unitOfWork = unitOfWork;
        _authService = authService;
        _mapper = mapper;
    }

    public async Task<List<GetUserTaskListDto>> Handle(GetUserTaskListQuery request, CancellationToken cancellationToken)
    {
        var result = await _authService.IsAdmin(request.UserId);

        if(result == false)
            throw new UnauthorizedException($"route authorized only for admins");

        var userTasks = await _unitOfWork.UserTaskRepository.GetAll();
        var userTaskDtos = _mapper.Map<List<GetUserTaskListDto>>(userTasks);
        return userTaskDtos;
    }
}



