using AutoMapper;
using HrManagement.Application.Features.AppRole.Commands.Create;
using HrManagement.Application.Features.AppRole.Queries.GetAllRoles;
using HrManagement.Domain.Entities.Identity;

namespace HrManagement.Application.Mapping;

public class RoleMappingProfile:Profile
{
    public RoleMappingProfile()
    {
        CreateMap<AppRole, GetAllRolesDto>().ReverseMap();
        CreateMap<AppRole, CreateRoleCommandRequest>().ReverseMap();
    }
}