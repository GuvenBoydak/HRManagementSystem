using FluentValidation;

namespace HrManagement.Application.Features.AppUser.Commands.Register;

public class RegisterAppUserCommandValidator:AbstractValidator<RegisterAppUserCommandRequest>
{
    public RegisterAppUserCommandValidator()
    {
        RuleFor(p=> p.FirstName).NotEmpty().WithMessage("FirstName can't be empty!");
        RuleFor(p=> p.Surname).NotEmpty().WithMessage("Surname can't be empty!");
        RuleFor(p=> p.UserName).NotEmpty().WithMessage("Username can't be null!");
        RuleFor(x => x.Email).NotEmpty().WithMessage("Email or username can't be empty.");
        RuleFor(x => x.Email).EmailAddress().WithMessage("Invalid email format.");
        RuleFor(x => x.Password).MinimumLength(6).WithMessage("Password must be at least 6 characters.");
        RuleFor(p => p.Password).Matches("[A-Z]").WithMessage("Password must contain at least 1 capital letter.");
        RuleFor(p => p.Password).Matches("[a-z]").WithMessage("Password must contain at least 1 lowercase letter.");
        
    }
}