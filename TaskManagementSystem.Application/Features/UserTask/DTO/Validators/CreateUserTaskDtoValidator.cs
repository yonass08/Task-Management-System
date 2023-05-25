using FluentValidation;
using TaskManagementSystem.Application.Contracts.Identity;
using TaskManagementSystem.Application.Contracts.Persistence;

namespace TaskManagementSystem.Application.Features.UserTask.DTO.Validators;

public class CreateUserTaskDtoValidator : BaseUserTaskValidator<CreateUserTaskDto>
{
    private readonly IUserService _userService;

    public CreateUserTaskDtoValidator(IUserService userService)
    {
        _userService = userService;

        RuleFor(x => x.UserId)
            .MustAsync(UserExists).WithMessage("User does not exist.");
    }

    private async Task<bool> UserExists(string userId, CancellationToken cancellationToken)
    {
        return await _userService.Exists(userId);
    }
}

