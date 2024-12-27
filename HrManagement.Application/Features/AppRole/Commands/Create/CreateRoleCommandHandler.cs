using HrManagement.Application.Interfaces.Services;

namespace HrManagement.Application.Features.AppRole.Commands.Create;

public class CreateRoleCommandHandler(IRoleService roleService):ICommandHandler<CreateRoleCommandRequest,CreateRoleCommandResponse>
{
    public async Task<CreateRoleCommandResponse> Handle(CreateRoleCommandRequest request, CancellationToken cancellationToken)
    {
        return new(await roleService.CreateAsync(request));
    }
}