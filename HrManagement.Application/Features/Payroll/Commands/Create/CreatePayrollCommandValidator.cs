using FluentValidation;

namespace HrManagement.Application.Features.Payroll.Commands.Create;

public class CreatePayrollCommandValidator:AbstractValidator<CreatePayrollCommandRequest>
{
    public CreatePayrollCommandValidator()
    {
        RuleFor(p => p.BasicSalary).NotEmpty().WithMessage("Basic Salary is required");
        RuleFor(p => p.Allowances).NotEmpty().WithMessage("Allowances is required");
        RuleFor(p => p.Deductions).NotEmpty().WithMessage("Deductions is required");
        RuleFor(p => p.Tax).NotEmpty().WithMessage("Tax is required");
        RuleFor(p => p.PayrollDate).NotEmpty().WithMessage("Payroll Date is required");
        RuleFor(p => p.BankAccountNumber).NotEmpty().WithMessage("Bank Account Number is required");
        RuleFor(p => p.RetirementFund).NotEmpty().WithMessage("Retirement Fund is required");
        RuleFor(p => p.EmployeeId).NotEmpty().WithMessage("Employee Id is required");
    }
}