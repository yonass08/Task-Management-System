using AutoMapper;
using MediatR;
using TaskManagementSystem.Application.Contracts.Persistence;
using TaskManagementSystem.Application.Exceptions;
using TaskManagementSystem.Application.Features.CheckList.CQRS.Requests.Commands;
using TaskManagementSystem.Application.Features.CheckList.DTO.Validators;

namespace TaskManagementSystem.Application.Features.CheckList.CQRS.Handlers.Commands;

public class UpdateCheckListCommandHandler : IRequestHandler<UpdateCheckListCommand, Unit>
{
    private readonly IUnitOfWork _unitOfWork;

    private readonly IMapper _mapper;

    public UpdateCheckListCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
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
        _mapper.Map(request.updateCheckListDto, CheckList);
        await UnitOfWork.CheckListRepository.Update(CheckList);

        if (await UnitOfWork.Save() < 0)
            throw new ActionNotPerfomedException("CheckList not Updated");
      
        return Unit.Value;

    }
}