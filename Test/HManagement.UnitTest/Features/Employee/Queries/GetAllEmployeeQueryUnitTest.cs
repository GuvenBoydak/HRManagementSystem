using HrManagement.Application;
using HrManagement.Application.Features.Employee.Queries.GetAllEmployee;
using HrManagement.Application.Features.LeaveForm.Dtos;
using HrManagement.Application.Features.Payroll.Dtos;
using HrManagement.Application.Features.Performance.Dtos;
using HrManagement.Application.Interfaces.Services;
using HrManagement.Domain.Enums;
using Moq;
using Shouldly;

namespace HManagement.UnitTest.Features.Employee.Queries;

public class GetAllEmployeeQueryUnitTest
{
    private readonly Mock<IEmployeeService> _employeeService;
    private readonly GetAllEmployeeQueryHandler _handler;

    public GetAllEmployeeQueryUnitTest()
    {
        _employeeService = new Mock<IEmployeeService>();
        _handler = new GetAllEmployeeQueryHandler(_employeeService.Object);
    }

    [Fact]
    public async Task Handle_ShouldReturnSuccessResponse_WhenEmployeesAreRetrievedSuccessfully()
    {
        // Arrange
        var employeeList = new List<GetAllEmployeeDto>
        {
            CreateEmployeeDto(),
            CreateEmployeeDto()
        };

        var serviceResult = ServiceResult<List<GetAllEmployeeDto>>.Success(employeeList);

        _employeeService
            .Setup(s => s.GetAllAsync())
            .ReturnsAsync(serviceResult);

        var request = new GetAllEmployeeQueryRequest();
        // Act
        var response = await _handler.Handle(request, default);

        // Assert
        response.ShouldNotBeNull();
        response.Response.ShouldBe(serviceResult);
        response.Response.Data.ShouldBe(employeeList);
        _employeeService.Verify(s => s.GetAllAsync(), Times.Once);
    }
    
    [Fact]
    public async Task Handle_ShouldReturnEmptyList_WhenNoEmployeesAreFound()
    {
        // Arrange
        var employeeList = new List<GetAllEmployeeDto>();

        var serviceResult = ServiceResult<List<GetAllEmployeeDto>>.Success(employeeList);

        _employeeService
            .Setup(s => s.GetAllAsync())
            .ReturnsAsync(serviceResult);

        var request = new GetAllEmployeeQueryRequest();
        // Act
        var response = await _handler.Handle(request, default);

        // Assert
        response.ShouldNotBeNull();
        response.Response.ShouldBe(serviceResult);
        response.Response.Data.ShouldBeEmpty();
        _employeeService.Verify(s => s.GetAllAsync(), Times.Once);
    }

    private GetAllEmployeeDto CreateEmployeeDto()
    {
        return new GetAllEmployeeDto(
            Name: "John",
            Surname: "Doe",
            Email: "john.doe@example.com",
            Phone: "123-456-7890",
            Gender: EmployeeGender.Male,
            DateOfBirth: new DateTime(1990, 1, 1),
            Address: "123 Main St",
            Position: "Developer",
            Department: EmployeeDepartment.Employee,
            Salary: 60000m,
            HireDate: new DateTime(2020, 1, 1),
            PerformanceScore: 85.5m,
            LeaveUsed: 5,
            CreatedDate: DateTime.UtcNow,
            UpdatedDate: DateTime.UtcNow,
            DeletedDate: DateTime.MinValue,
            IsDeleted: false,
            LeaveForms: new List<LeaveFormDto>(),
            Performances: new List<PerformanceDto>(),
            Payrolls: new List<PayrollDto>());
    }
}