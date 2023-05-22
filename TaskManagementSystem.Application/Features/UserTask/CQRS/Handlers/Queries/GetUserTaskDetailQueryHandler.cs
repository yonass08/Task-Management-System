using AutoMapper;
using MediatR;
using TaskManagementSystem.Application.Contracts.Identity;
using TaskManagementSystem.Application.Contracts.Persistence;
using TaskManagementSystem.Application.Exceptions;
using TaskManagementSystem.Application.Features.UserTask.CQRS.Requests.Queries;
using TaskManagementSystem.Application.Features.UserTask.DTO;

namespace TaskManagementSystem.Application.Features.UserTask.CQRS.Handlers.Queries;


public class GetUserTaskDetailQueryHandler : IRequestHandler<GetUserTaskDetailQuery, GetUserTaskDetailDto>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    private readonly IAuthorizationService _authService;

    public GetUserTaskDetailQueryHandler(IUnitOfWork unitOfWork, IMapper mapper, IAuthorizationService authService)
    {
        _unitOfWork = unitOfWork;
        _authService = authService;
        _mapper = mapper;
    }

    public async Task<GetUserTaskDetailDto> Handle(GetUserTaskDetailQuery request, CancellationToken cancellationToken)
    {
        var Exists = await _unitOfWork.UserTaskRepository.Exists(request.Id);
        if (Exists == false)
            throw new NotFoundException(nameof(Domain.UserTask), request.Id);

        var userTask = await _unitOfWork.UserTaskRepository.GetWithDetails(request.Id);
        var result = await _authService.UserTaskBelongsToUser(userTask, request.UserId);

        if(result == false)
            throw new UnauthorizedException($"ac = {userTask.UserId} and {request.UserId}");

        var userTaskDto = _mapper.Map<GetUserTaskDetailDto>(userTask);
        return userTaskDto;
    }
}