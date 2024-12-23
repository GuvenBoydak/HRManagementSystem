using AutoMapper;
using HrManagement.Application.Features.Employee.Commands.Create;
using HrManagement.Application.Features.Employee.Commands.Update;
using HrManagement.Application.Features.Employee.Queries.GetAllEmployee;
using HrManagement.Application.Features.Employee.Queries.GetEmployeeById;
using HrManagement.Domain.Entities;

namespace HrManagement.Application.Mapping;

public class EmployeeMappingProfile:Profile
{
    public EmployeeMappingProfile()
    {
        CreateMap<Employee, CreateEmployeeCommandRequest>().ReverseMap();
        CreateMap<Employee, UpdateEmployeeCommandRequest>().ReverseMap();
        CreateMap<Employee,GetAllEmployeeDto>().ReverseMap();
        CreateMap<Employee, GetEmployeeByIdDto>().ReverseMap();
    }
}