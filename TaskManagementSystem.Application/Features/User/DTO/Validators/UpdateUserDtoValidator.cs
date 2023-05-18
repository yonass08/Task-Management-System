using FluentValidation;
using TaskManagementSystem.Application.Contracts.Persistence;

namespace TaskManagementSystem.Application.Features.User.DTO.Validators;
public class UpdateUserDtoValidator : AbstractValidator<UpdateUserDto>
{
    private readonly IUserRepository _userRepository;

    public UpdateUserDtoValidator(IUserRepository userRepository)
    {
        _userRepository = userRepository;

        RuleFor(x => x.Id)
            .GreaterThan(0).WithMessage("Id must be greater than zero.")
            .MustAsync(Exist).WithMessage("User does not exist.");
        RuleFor(x => x.FullName)
            .NotEmpty().WithMessage("FullName can't be empty");

    }

    private async Task<bool> Exist(int id, CancellationToken cancellationToken)
    {
        return await _userRepository.Exists(id);
    }

}

