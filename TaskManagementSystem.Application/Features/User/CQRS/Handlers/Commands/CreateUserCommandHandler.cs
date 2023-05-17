using AutoMapper;
using MediatR;
using TaskManagementSystem.Domain;
using TaskManagementSystem.Application.Contracts.Persistence;
using TaskManagementSystem.Application.Exceptions;
using TaskManagementSystem.Application.Features.User.CQRS.Requests.Commands;
using TaskManagementSystem.Application.Features.User.DTO.Validators;

namespace TaskManagementSystem.Application.Features.User.CQRS.Handlers.Commands;

public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, int>
{
    private readonly IUnitOfWork _unitOfWork;

    private readonly IMapper _mapper;

    public CreateUserCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<int> Handle(CreateUserCommand request, CancellationToken cancellationToken)
    {

        var validator = new CreateUserDtoValidator(_unitOfWork.UserRepository);
        var validationResult = await validator.ValidateAsync(request.createUserDto);

             
        if (validationResult.IsValid == false)
            throw new ValidationException(validationResult);
    
        var user = _mapper.Map<Domain.User>(request.createUserDto);

        user = await _unitOfWork.UserRepository.Add(user);
        if (await _unitOfWork.Save() < 0)
            throw new ActionNotPerfomedException("user not created");
      
        return user.Id;

    }
}

