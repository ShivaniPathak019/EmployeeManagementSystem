using FluentValidation;

namespace EmployeeManagement.Application.Employees.Commands.CreateEmployee;

public class CreateEmployeeCommandValidator : AbstractValidator<CreateEmployeeCommand>
{
    public CreateEmployeeCommandValidator()
    {
        RuleFor(x => x.EmployeeCode)
            .NotEmpty().WithMessage("Employee code is required.")
            .MaximumLength(20).WithMessage("Employee code cannot exceed 20 characters.");

        RuleFor(x => x.FirstName)
            .NotEmpty().WithMessage("First name is required.")
            .MaximumLength(50).WithMessage("First name cannot exceed 50 characters.");

        RuleFor(x => x.LastName)
            .NotEmpty().WithMessage("Last name is required.")
            .MaximumLength(50).WithMessage("Last name cannot exceed 50 characters.");

        RuleFor(x => x.Email)
            .NotEmpty().WithMessage("Email is required.")
            .EmailAddress().WithMessage("Email format is invalid.")
            .MaximumLength(100).WithMessage("Email cannot exceed 100 characters.");

        RuleFor(x => x.PhoneNumber)
            .MaximumLength(20).WithMessage("Phone number cannot exceed 20 characters.");

        RuleFor(x => x.Address)
            .MaximumLength(250).WithMessage("Address cannot exceed 250 characters.");

        RuleFor(x => x.Salary)
            .GreaterThan(0).WithMessage("Salary must be greater than 0.");

        RuleFor(x => x.DateOfBirth)
            .LessThan(DateTime.Today).WithMessage("Date of birth must be in the past.");

        RuleFor(x => x.DateOfJoining)
            .GreaterThan(x => x.DateOfBirth)
            .WithMessage("Date of joining must be after date of birth.");

        RuleFor(x => x.DepartmentId)
            .GreaterThan(0).WithMessage("A valid department is required.");

        RuleFor(x => x.DesignationId)
            .GreaterThan(0).WithMessage("A valid designation is required.");
    }
}