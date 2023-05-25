using AutoMapper;
using MediatR;
using TaskManagementSystem.Application.Contracts.Identity;
using TaskManagementSystem.Application.Contracts.Persistence;
using TaskManagementSystem.Application.Exceptions;
using TaskManagementSystem.Application.Features.UserTask.CQRS.Requests.Commands;
using TaskManagementSystem.Application.Features.UserTask.DTO.Validators;

namespace TaskManagementSystem.Application.Features.UserTask.CQRS.Handlers.Commands;

public class CreateUserTaskCommandHandler : IRequestHandler<CreateUserTaskCommand, int>
{
    private readonly IUnitOfWork _unitOfWork;

    private readonly IMapper _mapper;
    private readonly IUserService _userService;
    private readonly IAuthorizationService _authService;

    public CreateUserTaskCommandHandler(IUnitOfWork unitOfWork, IMapper mapper, IUserService userService, IAuthorizationService authService)
    {
        _unitOfWork = unitOfWork;
        _userService = userService;
        _authService = authService;
        _mapper = mapper;
    }

    public async Task<int> Handle(CreateUserTaskCommand request, CancellationToken cancellationToken)
    {
        var result = await _authService.UserCanCreatTask(request.UserId, request.createUserTaskDto.UserId);
        if (result == false)
            throw new UnauthorizedException($"can't creat not mach");

        var validator = new CreateUserTaskDtoValidator(_userService);
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

