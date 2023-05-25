using AutoMapper;
using MediatR;
using TaskManagementSystem.Application.Contracts.Identity;
using TaskManagementSystem.Application.Contracts.Persistence;
using TaskManagementSystem.Application.Exceptions;
using TaskManagementSystem.Application.Features.CheckList.CQRS.Requests.Commands;
using TaskManagementSystem.Application.Features.CheckList.DTO.Validators;

namespace TaskManagementSystem.Application.Features.CheckList.CQRS.Handlers.Commands;

public class DeleteCheckListCommandHandler : IRequestHandler<DeleteCheckListCommand, Unit>
{
    private readonly IUnitOfWork _unitOfWork;

    private readonly IMapper _mapper;

    private readonly IAuthorizationService _authService;

    public DeleteCheckListCommandHandler(IUnitOfWork unitOfWork, IMapper mapper, IAuthorizationService authorizationService)
    {
        _unitOfWork = unitOfWork;
        _authService = authorizationService;
        _mapper = mapper;
    }

    public async Task<Unit> Handle(DeleteCheckListCommand request, CancellationToken cancellationToken)
    {

        var validator = new DeleteCheckListDtoValidator(_unitOfWork.CheckListRepository);
        var validationResult = await validator.ValidateAsync(request.deleteCheckListDto);
             
        if (validationResult.IsValid == false)
            throw new ValidationException(validationResult);

        var CheckList = await _unitOfWork.CheckListRepository.Get(request.deleteCheckListDto.Id);
        var UserTask = await _unitOfWork.UserTaskRepository.Get(CheckList.UserTaskId);
        
        var result = await _authService.UserTaskBelongsToUser(UserTask, request.UserId);

        if(result == false)
            throw new UnauthorizedException("Task doesn't belong to you");
    
        await _unitOfWork.CheckListRepository.Delete(CheckList);

        if (await _unitOfWork.Save() < 0)
            throw new ActionNotPerfomedException("CheckList not Deleted");
      
        return Unit.Value;

    }
}