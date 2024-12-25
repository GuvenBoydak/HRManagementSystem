using AutoMapper;
using HrManagement.Application.Features.LeaveForm.Commands.Create;
using HrManagement.Application.Features.LeaveForm.Commands.Update;
using HrManagement.Application.Features.LeaveForm.Queries.GetAllWithEmployeeId;
using HrManagement.Application.Features.LeaveForm.Queries.GetLeaveFormById;
using HrManagement.Domain.Entities;

namespace HrManagement.Application.Mapping;

public class LeaveFormMappingProfile : Profile
{
    public LeaveFormMappingProfile()
    {
        CreateMap<LeaveForm, GetLeaveFormByIdDto>().ReverseMap();
        CreateMap<LeaveForm, GetAllWithEmployeeIdDto>().ReverseMap();
        CreateMap<CreateLeaveFormCommandRequest, LeaveForm>().ReverseMap();
        CreateMap<UpdateLeaveFormCommandRequest, LeaveForm>().ReverseMap();
    }
}