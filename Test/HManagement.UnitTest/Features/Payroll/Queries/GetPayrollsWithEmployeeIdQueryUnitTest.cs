using HrManagement.Application;
using HrManagement.Application.Features.Employee.Dtos;
using HrManagement.Application.Features.Payroll.Queries.GetPayrollsWithEmployeeId;
using HrManagement.Application.Interfaces.Services;
using HrManagement.Domain.Enums;
using Moq;
using Shouldly;

namespace HManagement.UnitTest.Features.Payroll.Queries;

public class GetPayrollsWithEmployeeIdQueryUnitTest
{
    private readonly Mock<IPayrollService> _payrollService;
    private readonly GetPayrollsWithEmployeeIdQueryHandler _handler;

    public GetPayrollsWithEmployeeIdQueryUnitTest()
    {
        _payrollService = new Mock<IPayrollService>();
        _handler = new GetPayrollsWithEmployeeIdQueryHandler(_payrollService.Object);
    }

    [Fact]
    public async Task Handle_ShouldReturnSuccessResponse_WhenPayrollsAreRetrievedSuccessfully()
    {
        // Arrange
        var request = new GetPayrollsWithEmployeeIdQueryRequest(
            EmployeeId: Guid.NewGuid());
        var payrollList = GeneratePayrollDtos();

        var serviceResult = ServiceResult<List<GetPayrollsWithEmployeeIdDto>>.Success(payrollList);
        _payrollService
            .Setup(s => s.GetPayrollsWithEmployeeIdAsync(request.EmployeeId))
            .ReturnsAsync(serviceResult);

        // Act
        var response = await _handler.Handle(request, default);

        // Assert
        response.ShouldNotBeNull();
        response.Result.ShouldBe(serviceResult);
        response.Result.IsSuccess.ShouldBeTrue();
        _payrollService.Verify(s => s.GetPayrollsWithEmployeeIdAsync(request.EmployeeId), Times.Once);
    }

    [Fact]
    public async Task Handle_ShouldReturnEmptyList_WhenNoPayrollsAreFound()
    {
        // Arrange
        var request = new GetPayrollsWithEmployeeIdQueryRequest(
            EmployeeId: Guid.NewGuid());
        var payrollList = new List<GetPayrollsWithEmployeeIdDto>();

        var serviceResult = ServiceResult<List<GetPayrollsWithEmployeeIdDto>>.Success(payrollList);
        _payrollService
            .Setup(s => s.GetPayrollsWithEmployeeIdAsync(request.EmployeeId))
            .ReturnsAsync(serviceResult);

        // Act 
        var response = await _handler.Handle(request, default);

        // Assert
        response.ShouldNotBeNull();
        response.Result.ShouldBe(serviceResult);
        response.Result.Data.ShouldBeEmpty();
        _payrollService.Verify(s => s.GetPayrollsWithEmployeeIdAsync(request.EmployeeId), Times.Once);
    }

    private List<GetPayrollsWithEmployeeIdDto> GeneratePayrollDtos()
    {
        return new List<GetPayrollsWithEmployeeIdDto>
        {
            new GetPayrollsWithEmployeeIdDto(
                Id: Guid.NewGuid(),
                BasicSalary: 15000.00m,
                Allowances: 1000.00m,
                Overtime: 200.00m,
                Deductions: 150.00m,
                Tax: 500.00m,
                GrossSalary: 16050.00m,
                NetSalary: 15550.00m,
                PaymentDate: new DateTime(2023, 10, 1),
                BankAccountNumber: "1234567890",
                Comments: "Monthly payroll",
                RetirementFund: 300.00m,
                EmployeeId: Guid.NewGuid(),
                CreatedDate: DateTime.UtcNow,
                UpdatedDate: DateTime.UtcNow,
                DeletedDate: DateTime.MinValue,
                IsDeleted: false,
                Employee: new EmployeeDto(
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
                    IsDeleted: false)
            ),
            new GetPayrollsWithEmployeeIdDto(
                Id: Guid.NewGuid(),
                BasicSalary: 15000.00m,
                Allowances: 1000.00m,
                Overtime: 200.00m,
                Deductions: 150.00m,
                Tax: 500.00m,
                GrossSalary: 16050.00m,
                NetSalary: 15550.00m,
                PaymentDate: new DateTime(2023, 10, 1),
                BankAccountNumber: "1234567890",
                Comments: "Monthly payroll",
                RetirementFund: 300.00m,
                EmployeeId: Guid.NewGuid(),
                CreatedDate: DateTime.UtcNow,
                UpdatedDate: DateTime.UtcNow,
                DeletedDate: DateTime.MinValue,
                IsDeleted: false,
                Employee: new EmployeeDto(
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
                    IsDeleted: false)
            )
        };
    }
}