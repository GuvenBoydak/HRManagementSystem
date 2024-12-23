using System.Net;
using AutoMapper;
using HrManagement.Application.Constant;
using HrManagement.Application.Features.Employee.Commands.Create;
using HrManagement.Application.Features.Employee.Commands.Delete;
using HrManagement.Application.Features.Employee.Commands.Update;
using HrManagement.Application.Features.Employee.Queries.GetAllEmployee;
using HrManagement.Application.Features.Employee.Queries.GetEmployeeById;
using HrManagement.Application.Interfaces;
using HrManagement.Application.Interfaces.Repositories;
using HrManagement.Application.Interfaces.Services;
using HrManagement.Domain.Entities;

namespace HrManagement.Application.Services;

public class EmployeeService(IEmployeeRepository employeeRepository,IUnitOfWork unitOfWork,IMapper mapper):IEmployeeService
{
    public async Task<ServiceResult<List<GetAllEmployeeDto>>> GetAllAsync()
    {
        var employeeResponse = mapper.Map<List<GetAllEmployeeDto>>(await employeeRepository.GetAllAsync());
        
        return ServiceResult<List<GetAllEmployeeDto>>.Succes(employeeResponse);
    }

    public async Task<ServiceResult<GetEmployeeByIdDto>> GetByIdAsync(Guid id)
    {
       var employee = await employeeRepository.GetByIdAsync(id);
       if (employee is null)
       {
           return ServiceResult<GetEmployeeByIdDto>.Failure(EmployeeConstant.NotFound);
       }
       var employeeResponse = mapper.Map<GetEmployeeByIdDto>(employee);
       
       return ServiceResult<GetEmployeeByIdDto>.Succes(employeeResponse);
    }

    public async Task<ServiceResult<Guid>> AddAsync(CreateEmployeeCommandRequest commandRequest,CancellationToken cancellationToken)
    {
        var employee = mapper.Map<Employee>(commandRequest);
        
        await employeeRepository.AddAsync(employee,cancellationToken);
        await unitOfWork.SaveChangesAsync(cancellationToken);
        
        return ServiceResult<Guid>.SuccesAsCreated(employee.Id,$"api/employees/{employee.Id}");
    }

    public async Task<ServiceResult> UpdateAsync(UpdateEmployeeCommandRequest request)
    {
        var updatedEmployee = await employeeRepository.GetByIdAsync(request.Id);
        if (updatedEmployee is null)
        {
            return ServiceResult.Failure(EmployeeConstant.NotFound);
        }
        
        var employee = mapper.Map(request, updatedEmployee);
        
        employeeRepository.Update(employee);
        await unitOfWork.SaveChangesAsync();
        
        return ServiceResult.Succes(HttpStatusCode.NoContent);
    }

    public async Task<ServiceResult> DeleteAsync(DeleteEmployeeCommandRequest request)
    {
       var employee =  await employeeRepository.GetByIdAsync(request.Id);
       if (employee is null)
       {
           return ServiceResult.Failure(EmployeeConstant.NotFound);
       }
       employeeRepository.Remove(employee);
       await unitOfWork.SaveChangesAsync();

       return ServiceResult.Succes(HttpStatusCode.NoContent);
    }
}