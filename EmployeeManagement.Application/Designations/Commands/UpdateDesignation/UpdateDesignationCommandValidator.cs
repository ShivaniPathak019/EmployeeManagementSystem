using FluentValidation;

namespace EmployeeManagement.Application.Designations.Commands.UpdateDesignation;

public class UpdateDesignationCommandValidator : AbstractValidator<UpdateDesignationCommand>
{
    public UpdateDesignationCommandValidator()
    {
        RuleFor(x => x.Id)
            .GreaterThan(0).WithMessage("Id must be greater than 0.");

        RuleFor(x => x.Title)
            .NotEmpty().WithMessage("Title is required.")
            .MaximumLength(100).WithMessage("Title cannot exceed 100 characters.");

        RuleFor(x => x.Description)
            .MaximumLength(250).WithMessage("Description cannot exceed 250 characters.");
    }
}