using AutoMapper;
using MediatR;
using TaskManagementSystem.Application.Contracts.Persistence;
using TaskManagementSystem.Application.Exceptions;
using TaskManagementSystem.Application.Features.CheckList.CQRS.Requests.Commands;
using TaskManagementSystem.Application.Features.CheckList.DTO.Validators;

namespace TaskManagementSystem.Application.Features.User.CQRS.Handlers.Commands;

public class CreateCheckListCommandHandler : IRequestHandler<CreateCheckListCommand, int>
{
    private readonly IUnitOfWork _unitOfWork;

    private readonly IMapper _mapper;

    public CreateCheckListCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<int> Handle(CreateCheckListCommand request, CancellationToken cancellationToken)
    {

        var validator = new CreateCheckListDtoValidator(_unitOfWork.UserTaskRepository);
        var validationResult = await validator.ValidateAsync(request.createCheckListDto);

             
        if (validationResult.IsValid == false)
            throw new ValidationException(validationResult);
    
        var checkList = _mapper.Map<Domain.CheckList>(request.createCheckListDto);

        checkList = await _unitOfWork.CheckListRepository.Add(checkList);
        if (await _unitOfWork.Save() < 0)
            throw new ActionNotPerfomedException("checklist not created");
      
        return checkList.Id;

    }
}

