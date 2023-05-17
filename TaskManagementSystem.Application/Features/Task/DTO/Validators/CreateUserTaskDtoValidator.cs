using FluentValidation;
using TaskManagementSystem.Application.Contracts.Persistence;

namespace TaskManagementSystem.Application.Features.Task.DTO.Validators;

public class CreateUserTaskDTOValidator : BaseUserTaskValidator<CreateUserTaskDTO>
{
    private readonly IUserRepository _userRepository;

    public CreateUserTaskDTOValidator(IUserRepository userRepository)
    {
        _userRepository = userRepository;

        RuleFor(x => x.UserId)
            .MustAsync(UserExists).WithMessage("User does not exist.");
    }

    private async Task<bool> UserExists(int userId, CancellationToken cancellationToken)
    {
        return await _userRepository.Exists(userId);
    }
}

