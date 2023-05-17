using FluentValidation;
using TaskManagementSystem.Application.Contracts.Persistence;

namespace TaskManagementSystem.Application.Features.CheckList.DTO.Validators;

public class UpdateCheckListDtoValidator: BaseCheckListDtoValidator<UpdateCheckListDto>
{
    private ICheckListRepository _checkListRepository;

    public UpdateCheckListDtoValidator(ICheckListRepository checkListRepository)
    {
        _checkListRepository = checkListRepository;

        RuleFor(x => x.Id)
            .NotEmpty().WithMessage("Id cannot be empty.")
            .MustAsync(CheckListExists).WithMessage("User task does not exist.");
    }

    private async Task<bool> CheckListExists(int id, CancellationToken cancellationToken)
    {
        return await _checkListRepository.Exists(id);
    }
}