using AutoMapper;
using MediatR;
using TaskManagementSystem.Application.Contracts.Persistence;
using TaskManagementSystem.Application.Exceptions;
using TaskManagementSystem.Application.Features.CheckList.CQRS.Requests.Commands;
using TaskManagementSystem.Application.Features.CheckList.DTO.Validators;

namespace TaskManagementSystem.Application.Features.CheckList.CQRS.Handlers.Commands;

public class UpdateCheckListStatusCommandHandler : IRequestHandler<UpdateCheckListStatusCommand, Unit>
{
    private readonly IUnitOfWork _unitOfWork;

    private readonly IMapper _mapper;

    public UpdateCheckListStatusCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public IUnitOfWork UnitOfWork => _unitOfWork;

    public async Task<Unit> Handle(UpdateCheckListStatusCommand request, CancellationToken cancellationToken)
    {

        var validator = new UpdateCheckListStatusDtoValidator(UnitOfWork.CheckListRepository);
        var validationResult = await validator.ValidateAsync(request.updateCheckListStatusDto);

             
        if (validationResult.IsValid == false)
            throw new ValidationException(validationResult);
    
        var CheckList = await UnitOfWork.CheckListRepository.Get(request.updateCheckListStatusDto.Id);
        await UnitOfWork.CheckListRepository.UpdateStatus(CheckList, request.updateCheckListStatusDto.Status);

        if (await UnitOfWork.Save() < 0)
            throw new ActionNotPerfomedException("CheckListStatus not Updated");
      
        return Unit.Value;

    }
}