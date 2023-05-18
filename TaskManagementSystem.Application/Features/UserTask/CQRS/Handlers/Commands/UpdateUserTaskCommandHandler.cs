using AutoMapper;
using MediatR;
using TaskManagementSystem.Application.Contracts.Persistence;
using TaskManagementSystem.Application.Exceptions;
using TaskManagementSystem.Application.Features.UserTask.CQRS.Requests.Commands;
using TaskManagementSystem.Application.Features.UserTask.DTO.Validators;

namespace TaskManagementSystem.Application.Features.UserTask.CQRS.Handlers.Commands;

public class UpdateUserTaskCommandHandler : IRequestHandler<UpdateUserTaskCommand, Unit>
{
    private readonly IUnitOfWork _unitOfWork;

    private readonly IMapper _mapper;

    public UpdateUserTaskCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public IUnitOfWork UnitOfWork => _unitOfWork;

    public async Task<Unit> Handle(UpdateUserTaskCommand request, CancellationToken cancellationToken)
    {

        var validator = new UpdateUserTaskDtoValidator(UnitOfWork.UserTaskRepository);
        var validationResult = await validator.ValidateAsync(request.updateUserTaskDto);

             
        if (validationResult.IsValid == false)
            throw new ValidationException(validationResult);
    
        var UserTask = await UnitOfWork.UserTaskRepository.Get(request.updateUserTaskDto.Id);
        _mapper.Map(request.updateUserTaskDto, UserTask);
        await UnitOfWork.UserTaskRepository.Update(UserTask);

        if (await UnitOfWork.Save() < 0)
            throw new ActionNotPerfomedException("UserTask not Updated");
      
        return Unit.Value;

    }
}