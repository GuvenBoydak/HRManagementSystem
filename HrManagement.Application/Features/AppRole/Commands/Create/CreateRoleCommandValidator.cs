using FluentValidation;

namespace HrManagement.Application.Features.AppRole.Commands.Create;

public class CreateRoleCommandValidator:AbstractValidator<CreateRoleCommandRequest>
{
    public CreateRoleCommandValidator()
    {
        RuleFor(p=> p.Name).NotEmpty().NotNull().WithMessage("Role Name can't be empty");
    }
}