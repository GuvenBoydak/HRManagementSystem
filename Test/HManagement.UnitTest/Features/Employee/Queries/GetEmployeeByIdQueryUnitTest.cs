using HrManagement.Application;
using HrManagement.Application.Constant;
using HrManagement.Application.Features.Employee.Queries.GetEmployeeById;
using HrManagement.Application.Features.LeaveForm.Dtos;
using HrManagement.Application.Features.Payroll.Dtos;
using HrManagement.Application.Features.Performance.Dtos;
using HrManagement.Application.Interfaces.Services;
using HrManagement.Domain.Enums;
using Moq;
using Shouldly;

namespace HManagement.UnitTest.Features.Employee.Queries;

public class GetEmployeeByIdQueryUnitTest
{
    private readonly Mock<IEmployeeService> _employeeService;
    private readonly GetEmployeeByIdQueryHandler _handler;

    public GetEmployeeByIdQueryUnitTest()
    {
        _employeeService = new Mock<IEmployeeService>();
        _handler = new GetEmployeeByIdQueryHandler(_employeeService.Object);
    }

    [Fact]
    public async Task Handle_ShouldReturnSuccessResponse_WhenEmployeeRetrievedSuccessfully()
    {
        // Arrange
        var employee = CreateEmployeeDto();
        
        var serviceResult = ServiceResult<GetEmployeeByIdDto>.Success(employee);
        _employeeService.
            Setup(s=> s.GetByIdAsync(It.IsAny<Guid>()))
            .ReturnsAsync(serviceResult);
        
        var request = new GetEmployeeByIdQueryRequest(Guid.NewGuid());
        
        // Act
        var response = await _handler.Handle(request, default);
        
        // Assert
        response.ShouldNotBeNull();
        response.Response.ShouldBe(serviceResult);
        response.Response.Data.ShouldBe(employee);
        _employeeService.Verify(s => s.GetByIdAsync(It.IsAny<Guid>()), Times.Once);
    }
    [Fact]
    public async Task Handle_ShouldReturnEmpty_WhenNoEmployeeFound()
    {
        // Arrange
        var employeeId = Guid.NewGuid();
        
        var serviceResult = ServiceResult<GetEmployeeByIdDto>.Failure(EmployeeConstant.NotFound);
        _employeeService.
            Setup(s=> s.GetByIdAsync(employeeId))
            .ReturnsAsync(serviceResult);
        
        var request = new GetEmployeeByIdQueryRequest(employeeId);
        
        // Act
        var response = await _handler.Handle(request, default);
        
        // Assert
        response.ShouldNotBeNull();
        response.Response.ShouldBe(serviceResult);
        response.Response.Data.ShouldBeNull();
        _employeeService.Verify(s => s.GetByIdAsync(employeeId), Times.Once);
    }
    
    private GetEmployeeByIdDto CreateEmployeeDto()
    {
        return new GetEmployeeByIdDto(
            Id: Guid.NewGuid(),
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