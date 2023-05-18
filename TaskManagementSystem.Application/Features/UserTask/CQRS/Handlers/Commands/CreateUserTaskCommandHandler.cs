using AutoMapper;
using MediatR;
using TaskManagementSystem.Application.Contracts.Persistence;
using TaskManagementSystem.Application.Exceptions;
using TaskManagementSystem.Application.Features.UserTask.CQRS.Requests.Commands;
using TaskManagementSystem.Application.Features.UserTask.DTO.Validators;

namespace TaskManagementSystem.Application.Features.UserTask.CQRS.Handlers.Commands;

public class CreateUserTaskCommandHandler : IRequestHandler<CreateUserTaskCommand, int>
{
    private readonly IUnitOfWork _unitOfWork;

    private readonly IMapper _mapper;

    public CreateUserTaskCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<int> Handle(CreateUserTaskCommand request, CancellationToken cancellationToken)
    {

        var validator = new CreateUserTaskDtoValidator(_unitOfWork.UserRepository);
        var validationResult = await validator.ValidateAsync(request.createUserTaskDto);

             
        if (validationResult.IsValid == false)
            throw new ValidationException(validationResult);
    
        var userTask = _mapper.Map<Domain.UserTask>(request.createUserTaskDto);

        userTask = await _unitOfWork.UserTaskRepository.Add(userTask);
        if (await _unitOfWork.Save() < 0)
            throw new ActionNotPerfomedException("user not created");
      
        return userTask.Id;

    }
}

