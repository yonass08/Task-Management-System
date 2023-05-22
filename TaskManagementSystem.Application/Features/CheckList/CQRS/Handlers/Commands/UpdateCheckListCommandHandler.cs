using AutoMapper;
using MediatR;
using TaskManagementSystem.Application.Contracts.Identity;
using TaskManagementSystem.Application.Contracts.Persistence;
using TaskManagementSystem.Application.Exceptions;
using TaskManagementSystem.Application.Features.CheckList.CQRS.Requests.Commands;
using TaskManagementSystem.Application.Features.CheckList.DTO.Validators;

namespace TaskManagementSystem.Application.Features.CheckList.CQRS.Handlers.Commands;

public class UpdateCheckListCommandHandler : IRequestHandler<UpdateCheckListCommand, Unit>
{
    private readonly IUnitOfWork _unitOfWork;

    private readonly IMapper _mapper;

    private readonly IAuthorizationService _authService;

    public UpdateCheckListCommandHandler(IUnitOfWork unitOfWork, IMapper mapper, IAuthorizationService authorizationService)
    {
        _unitOfWork = unitOfWork;
        _authService = authorizationService;
        _mapper = mapper;
    }

    public IUnitOfWork UnitOfWork => _unitOfWork;

    public async Task<Unit> Handle(UpdateCheckListCommand request, CancellationToken cancellationToken)
    {

        var validator = new UpdateCheckListDtoValidator(UnitOfWork.CheckListRepository);
        var validationResult = await validator.ValidateAsync(request.updateCheckListDto);

             
        if (validationResult.IsValid == false)
            throw new ValidationException(validationResult);
    
        var CheckList = await UnitOfWork.CheckListRepository.Get(request.updateCheckListDto.Id);
        var UserTask = await _unitOfWork.UserTaskRepository.Get(CheckList.UserTaskId);
        
        var result = await _authService.UserTaskBelongsToUser(UserTask, request.UserId);
        if(result == false)
            throw new UnauthorizedException("Task doesn't belong to you");
            
        _mapper.Map(request.updateCheckListDto, CheckList);
        await UnitOfWork.CheckListRepository.Update(CheckList);

        if (await UnitOfWork.Save() < 0)
            throw new ActionNotPerfomedException("CheckList not Updated");
      
        return Unit.Value;

    }
}