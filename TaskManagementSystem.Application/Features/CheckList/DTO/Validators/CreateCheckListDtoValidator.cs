using FluentValidation;
using TaskManagementSystem.Application.Contracts.Persistence;

namespace TaskManagementSystem.Application.Features.CheckList.DTO.Validators;


public class CreateCheckListDtoValidator : BaseCheckListDtoValidator<CreateCheckListDto>
{
    private readonly IUserTaskRepository _userTaskRepository;

    public CreateCheckListDtoValidator(IUserTaskRepository userTaskRepository)
    {
        _userTaskRepository = userTaskRepository;

        RuleFor(x => x.UserTaskId)
            .MustAsync(UserTaskExists).WithMessage("UserTask does not exist.");
    }

    private async Task<bool> UserTaskExists(int userId, CancellationToken cancellationToken)
    {
        return await _userTaskRepository.Exists(userId);
    }
}

