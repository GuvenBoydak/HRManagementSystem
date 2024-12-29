using FluentValidation;

namespace HrManagement.Application.Features.AppUser.Commands.Login;

public class LoginAppUserCommandValidator:AbstractValidator<LoginAppUserCommandRequest>
{
    public LoginAppUserCommandValidator()
    {
        RuleFor(x => x.EmailOrUserName).NotEmpty().WithMessage("Email or username can't be empty.");
        RuleFor(x => x.Password).MinimumLength(6).WithMessage("Password must be at least 6 characters.");
        RuleFor(p => p.Password).Matches("[A-Z]").WithMessage("Password must contain at least 1 capital letter.");
        RuleFor(p => p.Password).Matches("[a-z]").WithMessage("Password must contain at least 1 lowercase letter.");
    } 
}