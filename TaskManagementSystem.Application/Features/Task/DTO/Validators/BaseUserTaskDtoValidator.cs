using FluentValidation;

namespace TaskManagementSystem.Application.Features.Task.DTO.Validators;

public class BaseUserTaskValidator<T> : AbstractValidator<T> where T : BaseUserTaskDto
{
    public BaseUserTaskValidator()
    {
        RuleFor(x => x.Title)
            .NotEmpty().WithMessage("Title cannot be empty");

        RuleFor(x => x.Description)
            .NotEmpty().WithMessage("Description cannot be empty");

        RuleFor(x => x.StartDate)
            .LessThan(x => x.EndDate).WithMessage("StartDate must be before EndDate");

        RuleFor(x => x.EndDate)
            .GreaterThan(x => x.StartDate).WithMessage("EndDate must be after StartDate");
    }
}

