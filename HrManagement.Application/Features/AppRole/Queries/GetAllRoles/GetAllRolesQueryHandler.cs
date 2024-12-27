using HrManagement.Application.Interfaces.Services;

namespace HrManagement.Application.Features.AppRole.Queries.GetAllRoles;

public class GetAllRolesQueryHandler(IRoleService roleService):IQueryHandler<GetAllRolesQueryRequest,GetAllRolesQueryResponse>
{
    public async Task<GetAllRolesQueryResponse> Handle(GetAllRolesQueryRequest request, CancellationToken cancellationToken)
    {
        return new(await roleService.GetAllAsync());
    }
}