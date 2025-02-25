using AutoMapper;
using HrManagement.Application.Features.AppUser.Dtos;
using HrManagement.Domain.Entities.Identity;

namespace HrManagement.Application.Mapping;

public class AppUserMappingProfile : Profile
{
    public AppUserMappingProfile()
    {
        CreateMap<AppUser, AppUserDto>().ReverseMap();
    }
}