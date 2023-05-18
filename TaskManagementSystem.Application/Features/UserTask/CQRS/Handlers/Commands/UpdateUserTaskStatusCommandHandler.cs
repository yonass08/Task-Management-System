using AutoMapper;
using MediatR;
using TaskManagementSystem.Application.Contracts.Persistence;
using TaskManagementSystem.Application.Exceptions;
using TaskManagementSystem.Application.Features.UserTask.CQRS.Requests.Commands;
using TaskManagementSystem.Application.Features.UserTask.DTO.Validators;

namespace TaskManagementSystem.Application.Features.UserTaskStatus.CQRS.Handlers.Commands;

public class UpdateUserTaskStatusCommandHandler : IRequestHandler<UpdateUserTaskStatusCommand, Unit>
{
    private readonly IUnitOfWork _unitOfWork;

    private readonly IMapper _mapper;

    public UpdateUserTaskStatusCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public IUnitOfWork UnitOfWork => _unitOfWork;

    public async Task<Unit> Handle(UpdateUserTaskStatusCommand request, CancellationToken cancellationToken)
    {

        var validator = new UpdateUserTaskStatusDtoValidator(UnitOfWork.UserTaskRepository);
        var validationResult = await validator.ValidateAsync(request.updateUserTaskStatusDto);

             
        if (validationResult.IsValid == false)
            throw new ValidationException(validationResult);
    
        var UserTask = await UnitOfWork.UserTaskRepository.Get(request.updateUserTaskStatusDto.Id);
        await UnitOfWork.UserTaskRepository.UpdateStatus(UserTask, request.updateUserTaskStatusDto.Status);

        if (await UnitOfWork.Save() < 0)
            throw new ActionNotPerfomedException("UserTaskStatus not Updated");
      
        return Unit.Value;

    }
}