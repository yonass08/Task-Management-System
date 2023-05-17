using AutoMapper;
using MediatR;
using TaskManagementSystem.Application.Contracts.Persistence;
using TaskManagementSystem.Application.Exceptions;
using TaskManagementSystem.Application.Features.User.CQRS.Requests.Commands;
using TaskManagementSystem.Application.Features.User.DTO.Validators;

namespace TaskManagementSystem.Application.Features.User.CQRS.Handlers.Commands;


public class UpdateUserCommandHandler : IRequestHandler<UpdateUserCommand, Unit>
{
    private readonly IUnitOfWork _unitOfWork;

    private readonly IMapper _mapper;

    public UpdateUserCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public IUnitOfWork UnitOfWork => _unitOfWork;

    public async Task<Unit> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
    {

        var validator = new UpdateUserDtoValidator(UnitOfWork.UserRepository);
        var validationResult = await validator.ValidateAsync(request.updateUserDto);

             
        if (validationResult.IsValid == false)
            throw new ValidationException(validationResult);
    
        var user = await UnitOfWork.UserRepository.Get(request.updateUserDto.Id);
        await UnitOfWork.UserRepository.Update(user);

        if (await UnitOfWork.Save() < 0)
            throw new ActionNotPerfomedException("user not Updated");
      
        return Unit.Value;

    }
}