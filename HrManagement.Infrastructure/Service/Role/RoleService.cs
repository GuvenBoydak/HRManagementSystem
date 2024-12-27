using System.Net;
using AutoMapper;
using HrManagement.Application;
using HrManagement.Application.Constant;
using HrManagement.Application.Features.AppRole.Commands.AssingRoleToUser;
using HrManagement.Application.Features.AppRole.Commands.Create;
using HrManagement.Application.Features.AppRole.Queries.GetAllRoles;
using HrManagement.Application.Interfaces.Services;
using HrManagement.Domain.Entities.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace HrManagement.Infrastructure.Service.Role;

public class RoleService(RoleManager<AppRole> roleManager, UserManager<AppUser> userManager, IMapper mapper)
    : IRoleService
{
    public async Task<ServiceResult<List<GetAllRolesDto>>> GetAllAsync()
    {
        var roles = await roleManager.Roles.ToListAsync();
        var roleDtos = mapper.Map<List<GetAllRolesDto>>(roles);

        return ServiceResult<List<GetAllRolesDto>>.Success(roleDtos);
    }

    public async Task<ServiceResult> CreateAsync(CreateRoleCommandRequest request)
    {
        if (await roleManager.RoleExistsAsync(request.Name))
        {
            return ServiceResult.Failure(RoleConstant.ExistRole, HttpStatusCode.BadRequest);
        }

        var role = mapper.Map<AppRole>(request);
        var result = await roleManager.CreateAsync(role);

        if (!result.Succeeded)
        {
            return ServiceResult.Failure(string.Join(", ", result.Errors.Select(x => x.Description)),
                HttpStatusCode.BadRequest);
        }

        return ServiceResult.Success(HttpStatusCode.NoContent);
    }

    public async Task<ServiceResult> AssignRoleToUserAsync(AssignRoleToUserCommandRequest request)
    {
        var isRoleExist = await roleManager.RoleExistsAsync(request.RoleName);
        if (!isRoleExist)
        {
            return ServiceResult.Failure(RoleConstant.NotFound, HttpStatusCode.NotFound);
        }

        var user = await userManager.FindByIdAsync(request.UserId.ToString());
        if (user is null)
        {
            return ServiceResult.Failure(UserConstant.NotFound, HttpStatusCode.NotFound);
        }

        var result = await userManager.AddToRoleAsync(user, request.RoleName);
        if (!result.Succeeded)
        {
            return ServiceResult.Failure(string.Join(", ", result.Errors.Select(x => x.Description)),
                HttpStatusCode.BadRequest);
        }

        return ServiceResult.Success(HttpStatusCode.NoContent);
    }
}