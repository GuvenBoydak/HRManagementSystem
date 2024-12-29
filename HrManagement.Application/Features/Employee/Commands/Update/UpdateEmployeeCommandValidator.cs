using FluentValidation;

namespace HrManagement.Application.Features.Employee.Commands.Update;

public class UpdateEmployeeCommandValidator:AbstractValidator<UpdateEmployeeCommandRequest>
{
    public UpdateEmployeeCommandValidator()
    {
        RuleFor(p => p.Name).NotEmpty().WithMessage("Name is required");
        RuleFor(p => p.Surname).NotEmpty().WithMessage("Surname is required");
        RuleFor(p => p.Email).NotEmpty().WithMessage("Email is required");
        RuleFor(p => p.Phone).NotEmpty().WithMessage("Phone is required");
        RuleFor(p => p.Gender).NotEmpty().WithMessage("Gender is required");
        RuleFor(p => p.DateOfBirth).NotEmpty().WithMessage("DateOfBirth is required");
        RuleFor(p => p.Address).NotEmpty().WithMessage("Address is required");
        RuleFor(p => p.Position).NotEmpty().WithMessage("Position is required");
        RuleFor(p => p.Department).NotEmpty().WithMessage("Department is required");
        RuleFor(p => p.Salary).NotEmpty().WithMessage("Salary is required");
        RuleFor(p => p.HireDate).NotEmpty().WithMessage("HireDate is required");
    }
}