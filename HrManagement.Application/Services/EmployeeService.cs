using System.Net;
using AutoMapper;
using HrManagement.Application.Constant;
using HrManagement.Application.Features.AppRole.Commands.AssingRoleToUser;
using HrManagement.Application.Features.Employee.Commands.Create;
using HrManagement.Application.Features.Employee.Commands.Delete;
using HrManagement.Application.Features.Employee.Commands.Update;
using HrManagement.Application.Features.Employee.Queries.GetAllEmployee;
using HrManagement.Application.Features.Employee.Queries.GetEmployeeById;
using HrManagement.Application.Interfaces;
using HrManagement.Application.Interfaces.Repositories;
using HrManagement.Application.Interfaces.Services;
using HrManagement.Domain.Entities;
using HrManagement.Domain.Entities.Identity;
using HrManagement.Domain.Enums;
using HrManagement.Domain.Shared;

namespace HrManagement.Application.Services;

public class EmployeeService(
    IEmployeeRepository employeeRepository,
    IUnitOfWork unitOfWork,
    IMapper mapper,
    IRoleService roleService,
    IAuthService authService)
    : IEmployeeService
{
    public async Task<ServiceResult<List<GetAllEmployeeDto>>> GetAllAsync()
    {
        var employeeResponse = mapper.Map<List<GetAllEmployeeDto>>(await employeeRepository.GetAllAsync(false,
            x => x.LeaveForms,
            x => x.Performances,
            x => x.Payrolls));

        return ServiceResult<List<GetAllEmployeeDto>>.Success(employeeResponse);
    }

    public async Task<ServiceResult<PaginationResponse<List<GetAllEmployeeDto>>>> GetEmployeesBySearchWithPaginationAsync(string search,int pageNumber, int pageSize,CancellationToken cancellationToken)
    {
        var result = mapper.Map<List<GetAllEmployeeDto>>( await employeeRepository.GetEmployeesBySearchWithPaginationAsync(search,pageNumber, pageSize));
        var paginationResponse = new PaginationResponse<List<GetAllEmployeeDto>>
        {
            Data = result,
            PageNumber = pageNumber,
            PageSize = pageSize,
            totalCount = await employeeRepository.CountAsync(cancellationToken)
        };

        return ServiceResult<PaginationResponse<List<GetAllEmployeeDto>>>.Success(paginationResponse);
    }

    public async Task<ServiceResult<GetEmployeeByIdDto>> GetByIdAsync(Guid id)
    {
        var employee = await employeeRepository.GetByIdAsync(id, false,
            x => x.LeaveForms,
            x => x.Performances,
            x => x.Payrolls);
        if (employee is null)
        {
            return ServiceResult<GetEmployeeByIdDto>.Failure(EmployeeConstant.NotFound);
        }

        var employeeResponse = mapper.Map<GetEmployeeByIdDto>(employee);

        return ServiceResult<GetEmployeeByIdDto>.Success(employeeResponse);
    }

    public async Task<ServiceResult<Guid>> AddAsync(CreateEmployeeCommandRequest commandRequest,
        CancellationToken cancellationToken)
    {
        var employee = mapper.Map<Employee>(commandRequest);

        await employeeRepository.AddAsync(employee, cancellationToken);
        
        int affectedRows = await unitOfWork.SaveChangesAsync(cancellationToken);
        if (affectedRows <= 0)
        {
            return ServiceResult<Guid>.Failure(EmployeeConstant.FailedToSave, HttpStatusCode.InternalServerError);
        }

        var response = await CreateAppUser(employee);
        if (response.IsFailure)
        {
            return ServiceResult<Guid>.Failure(response.ErrorMessages, response.StatusCode);
        }
        
        var result = await AssignRoleToEmployeeAsync(employee);
        if (result.IsFailure)
        {
            return ServiceResult<Guid>.Failure(result.ErrorMessages, result.StatusCode);
        }

        return ServiceResult<Guid>.SuccessAsCreated(employee.Id, $"api/employees/{employee.Id}");
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

        return ServiceResult.Success(HttpStatusCode.NoContent);
    }

    public async Task<ServiceResult> DeleteAsync(DeleteEmployeeCommandRequest request)
    {
        var employee = await employeeRepository.GetByIdAsync(request.Id);
        if (employee is null)
        {
            return ServiceResult.Failure(EmployeeConstant.NotFound);
        }

        employeeRepository.Remove(employee);
        await unitOfWork.SaveChangesAsync();

        return ServiceResult.Success(HttpStatusCode.NoContent);
    }

    private async Task<ServiceResult> AssignRoleToEmployeeAsync(Employee employee)
    {
        var user = await authService.GetByIdAsync(employee.Id);

        var roleName = employee.Department switch
        {
            EmployeeDepartment.Employee => EmployeeConstant.Employee,
            EmployeeDepartment.HumanResources => EmployeeConstant.HumanResources,
            EmployeeDepartment.Manager => EmployeeConstant.Manager,
            _ => null
        };

        if (roleName is null)
        {
            return ServiceResult.Failure(RoleConstant.NotFound, HttpStatusCode.NotFound);
        }

        return await roleService.AssignRoleToUserAsync(
            new AssignRoleToUserCommandRequest(RoleName: roleName, UserId: user.Data.Id));
    }

    private async Task<ServiceResult> CreateAppUser(Employee employee)
    {
        var user = new AppUser
        {
            Id = employee.Id,
            NameSurname = employee.Name + employee.Surname,
            UserName = employee.Email,
            Email = employee.Email
        };
        
        return await authService.RegisterAsync(user,"HumanR1,");
    }
}