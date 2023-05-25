using AutoMapper;
using MediatR;
using TaskManagementSystem.Application.Contracts.Identity;
using TaskManagementSystem.Application.Contracts.Persistence;
using TaskManagementSystem.Application.Exceptions;
using TaskManagementSystem.Application.Features.UserTask.CQRS.Requests.Commands;
using TaskManagementSystem.Application.Features.UserTask.DTO.Validators;

namespace TaskManagementSystem.Application.Features.UserTask.CQRS.Handlers.Commands;

public class DeleteUserTaskCommandHandler : IRequestHandler<DeleteUserTaskCommand, Unit>
{
    private readonly IUnitOfWork _unitOfWork;

    private readonly IMapper _mapper;

    private readonly IAuthorizationService _authService;

    public DeleteUserTaskCommandHandler(IUnitOfWork unitOfWork, IMapper mapper, IAuthorizationService authService)
    {
        _unitOfWork = unitOfWork;
        _authService = authService;
        _mapper = mapper;
    }


    public async Task<Unit> Handle(DeleteUserTaskCommand request, CancellationToken cancellationToken)
    {

        var validator = new DeleteUserTaskDtoValidator(_unitOfWork.UserTaskRepository);
        var validationResult = await validator.ValidateAsync(request.deleteUserTaskDto);

             
        if (validationResult.IsValid == false)
            throw new ValidationException(validationResult);
    
        var UserTask = await _unitOfWork.UserTaskRepository.Get(request.deleteUserTaskDto.Id);
        var result = await _authService.UserTaskBelongsToUser(UserTask, request.UserId);

        if(result == false)
            throw new UnauthorizedException($"ac = {UserTask.UserId} and {request.UserId}");
        
        await _unitOfWork.UserTaskRepository.Delete(UserTask);

        if (await _unitOfWork.Save() < 0)
            throw new ActionNotPerfomedException("UserTask not Deleted");
      
        return Unit.Value;

    }
}