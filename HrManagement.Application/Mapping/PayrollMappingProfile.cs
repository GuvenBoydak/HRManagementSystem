using AutoMapper;
using HrManagement.Application.Features.Payroll.Commands.Create;
using HrManagement.Application.Features.Payroll.Commands.Update;
using HrManagement.Application.Features.Payroll.Queries.GetPayrollById;
using HrManagement.Application.Features.Payroll.Queries.GetPayrollsWithEmployeeId;
using HrManagement.Domain.Entities;

namespace HrManagement.Application.Mapping;

public class PayrollMappingProfile: Profile
{
    public PayrollMappingProfile()
    {
        CreateMap<Payroll, GetPayrollsWithEmployeeIdDto>().ReverseMap();
        CreateMap<Payroll, GetPayrollByIdDto>().ReverseMap();
        CreateMap<Payroll,CreatePayrollCommandRequest>().ReverseMap();
        CreateMap<Payroll,UpdatePayrollCommandRequest>().ReverseMap();
    }
}