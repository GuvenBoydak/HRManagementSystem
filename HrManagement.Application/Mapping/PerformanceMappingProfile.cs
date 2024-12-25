using AutoMapper;
using HrManagement.Application.Features.Performance.Commands.Create;
using HrManagement.Application.Features.Performance.Commands.Update;
using HrManagement.Application.Features.Performance.Queries.GetAllPerformanceWithEmployeeId;
using HrManagement.Application.Features.Performance.Queries.GetPerformanceById;
using HrManagement.Domain.Entities;

namespace HrManagement.Application.Mapping;

public class PerformanceMappingProfile: Profile
{
    public PerformanceMappingProfile()
    {
        CreateMap<Performance, GetAllPerformanceWithEmployeeIdDto>().ReverseMap();
        CreateMap<Performance, GetPerformanceByIdDto>().ReverseMap();
        CreateMap<CreatePerformanceCommandRequest,Performance>().ReverseMap();
        CreateMap<UpdatePerformanceCommandRequest,Performance>().ReverseMap();
    }
}