using AutoMapper;
using MediatR;
using TaskManagementSystem.Application.Contracts.Persistence;
using TaskManagementSystem.Application.Exceptions;
using TaskManagementSystem.Application.Features.User.CQRS.Requests.Commands;
using TaskManagementSystem.Application.Features.User.DTO.Validators;

namespace TaskManagementSystem.Application.Features.User.CQRS.Handlers.Commands;

public class DeleteUserCommandHandler : IRequestHandler<DeleteUserCommand, Unit>
{
    private readonly IUnitOfWork _unitOfWork;

    private readonly IMapper _mapper;

    public DeleteUserCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<Unit> Handle(DeleteUserCommand request, CancellationToken cancellationToken)
    {

        var validator = new DeleteUserDtoValidator(_unitOfWork.UserRepository);
        var validationResult = await validator.ValidateAsync(request.deleteUserDto);

             
        if (validationResult.IsValid == false)
            throw new ValidationException(validationResult);
    
        var user = await _unitOfWork.UserRepository.Get(request.deleteUserDto.Id);
        await _unitOfWork.UserRepository.Delete(user);

        if (await _unitOfWork.Save() < 0)
            throw new ActionNotPerfomedException("user not Deleted");
      
        return Unit.Value;

    }
}