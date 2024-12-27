using HrManagement.Application.Interfaces.Services;

namespace HrManagement.Application.Features.AppRole.Commands.AssingRoleToUser;

public class AssignRoleToUserCommandHandler(IRoleService roleService):ICommandHandler<AssignRoleToUserCommandRequest,AssignRoleToUserCommandResponse>
{
    public async Task<AssignRoleToUserCommandResponse> Handle(AssignRoleToUserCommandRequest request, CancellationToken cancellationToken)
    {
        return new(await roleService.AssignRoleToUserAsync(request));
    }
}