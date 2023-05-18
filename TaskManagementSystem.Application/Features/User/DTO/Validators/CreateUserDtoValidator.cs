using FluentValidation;
using TaskManagementSystem.Application.Contracts.Persistence;

namespace TaskManagementSystem.Application.Features.User.DTO.Validators;

public class CreateUserDtoValidator : AbstractValidator<CreateUserDto>
{
    private readonly IUserRepository _userRepository;

    public CreateUserDtoValidator(IUserRepository userRepository)
    {
        _userRepository = userRepository;

        RuleFor(x => x.FullName)
            .NotEmpty().WithMessage("First name is required.");

        RuleFor(x => x.Email)
            .NotEmpty().WithMessage("Email is required.")
            .EmailAddress().WithMessage("Invalid email format.")
            .MustAsync(BeUniqueEmail).WithMessage("Email is already taken.");
    }

    private async Task<bool> BeUniqueEmail(string email, CancellationToken cancellationToken)
    {
        var user = await _userRepository.GetByEmail(email);
        return user == null;
    }
}
