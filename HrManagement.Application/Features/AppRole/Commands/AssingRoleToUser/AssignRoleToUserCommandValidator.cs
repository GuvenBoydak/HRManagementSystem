using FluentValidation;

namespace HrManagement.Application.Features.AppRole.Commands.AssingRoleToUser;

public class AssignRoleToUserCommandValidator:AbstractValidator<AssignRoleToUserCommandRequest>
{
    public AssignRoleToUserCommandValidator()
    {
        RuleFor(p=> p.UserId).NotEmpty().NotNull().WithMessage("UserId can't be empty");
        RuleFor(p=> p.RoleName).NotEmpty().NotNull().WithMessage("Role Name can't be empty");
    }
}