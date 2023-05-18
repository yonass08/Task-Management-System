using FluentValidation;

namespace TaskManagementSystem.Application.Features.CheckList.DTO.Validators;

public class BaseCheckListDtoValidator<T> : AbstractValidator<T> where T: BaseCheckListDto
{
    public BaseCheckListDtoValidator()
    {
        RuleFor(x => x.Title)
            .NotEmpty().WithMessage("Title cannot be empty.")
            .MaximumLength(50).WithMessage("Title cannot exceed 50 characters.");

        RuleFor(x => x.Description)
            .NotEmpty().WithMessage("Description cannot be empty.")
            .MaximumLength(200).WithMessage("Description cannot exceed 200 characters.");
    }
}

