using FluentValidation;
using TaskManagementSystem.Application.Contracts.Persistence;

namespace TaskManagementSystem.Application.Features.UserTask.DTO.Validators;

public class UpdateUserTaskDtoValidator : BaseUserTaskValidator<UpdateUserTaskDto>
{
    private IUserTaskRepository _userTaskRepository;

    public UpdateUserTaskDtoValidator(IUserTaskRepository userTaskRepository)
    {
        _userTaskRepository = userTaskRepository;

        RuleFor(x => x.Id)
            .NotEmpty().WithMessage("Id cannot be empty.")
            .MustAsync(UserTaskExists).WithMessage("User task does not exist.");
    }

    private async Task<bool> UserTaskExists(int id, CancellationToken cancellationToken)
    {
        return await _userTaskRepository.Exists(id);
    }
}
