using FluentValidation;
using TaskManagementSystem.Application.Contracts.Persistence;

namespace TaskManagementSystem.Application.Features.User.DTO.Validators;

public class DeleteUserDtoValidator : AbstractValidator<DeleteUserDto>
{
    private readonly IUserRepository _userRepository;

    public DeleteUserDtoValidator(IUserRepository userRepository)
    {
        _userRepository = userRepository;

        RuleFor(x => x.Id)
            .GreaterThan(0).WithMessage("Id must be greater than zero.")
            .MustAsync(Exist).WithMessage("User does not exist.");
    }

    private async Task<bool> Exist(int id, CancellationToken cancellationToken)
    {
        return await _userRepository.Exists(id);
    }
}