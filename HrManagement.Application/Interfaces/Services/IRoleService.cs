using HrManagement.Application.Features.AppRole.Commands.AssingRoleToUser;
using HrManagement.Application.Features.AppRole.Commands.Create;
using HrManagement.Application.Features.AppRole.Queries.GetAllRoles;

namespace HrManagement.Application.Interfaces.Services;

public interface IRoleService
{
    Task<ServiceResult<List<GetAllRolesDto>>> GetAllAsync();
    Task<ServiceResult> CreateAsync(CreateRoleCommandRequest request);
    Task<ServiceResult> AssignRoleToUserAsync(AssignRoleToUserCommandRequest request);
}