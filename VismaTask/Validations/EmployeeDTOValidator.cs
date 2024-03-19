using FluentValidation;
using VismaTask.Constats;
using VismaTask.Models.DTOs;

namespace VismaTask.Validations;

public class EmployeeDTOValidator : AbstractValidator<EmployeeDTO>
{
    public EmployeeDTOValidator()
    {
        {
            RuleFor(x => x.CurrentSalary)
                .NotEmpty().WithMessage("Salary can not be empty")
                .GreaterThan(0).WithMessage("Salary can not be negative");
            RuleFor(x => x.EmploymentDate)
                .NotNull().WithMessage("Employment date can not be null")
                .NotEmpty().WithMessage("Employment date can not be empty")
                .GreaterThanOrEqualTo(EmploymentDateRequirements.MinEmploymentDate).WithMessage("EmploymentDate cannot be earlier than 2000-01-01")
                .LessThan(DateTime.Now).WithMessage("EmploymentDate cannot be a future date");
            RuleFor(x => x.Birthdate)
                .NotEmpty()
                .NotNull()
                .GreaterThanOrEqualTo(DateTime.Now.AddYears(-EmployeeAgeRequirements.MinEmployeeAge)).WithMessage("Employee must be at least 18 years")
                .LessThanOrEqualTo(DateTime.Now.AddYears(-EmployeeAgeRequirements.MaxEmployeeAge)).WithMessage("Employee must be not older than 70 years");
            RuleFor(x => x.HomeAddress)
                .NotEmpty().WithMessage("Home address can not be null")
                .NotNull().WithMessage("Home address can not be empty");
            RuleFor(x => x.Role)
                 .NotEmpty().WithMessage("Role can not be null");

        }
    }
}
