using System.Net;
using HrManagement.Application;
using HrManagement.Application.Constant;
using HrManagement.Application.Features.Employee.Dtos;
using HrManagement.Application.Features.Payroll.Queries.GetPayrollById;
using HrManagement.Application.Interfaces.Services;
using HrManagement.Domain.Enums;
using Moq;
using Shouldly;

namespace HManagement.UnitTest.Features.Payroll.Queries;

public class GetPayrollByIdQueryUnitTest
{
    private readonly Mock<IPayrollService> _payrollService;
    private readonly GetPayrollByIdQueryHandler _handler;

    public GetPayrollByIdQueryUnitTest()
    {
        _payrollService = new Mock<IPayrollService>();
        _handler = new GetPayrollByIdQueryHandler(_payrollService.Object);
    }

    [Fact]
    public async Task Handle_ShouldReturnSuccessResponse_WhenPayrollsRetrievedSuccessfully()
    {
        // Arrange 
        var request = new GetPayrollByIdQueryRequest(Id: Guid.NewGuid());
        var payrollDtos = GeneratePayrollDto();

        var serviceResult = ServiceResult<GetPayrollByIdDto>.Success(payrollDtos);
        _payrollService
            .Setup(s => s.GetPayrollByIdAsync(request.Id))
            .ReturnsAsync(serviceResult);

        // Act 
        var response = await _handler.Handle(request, default);

        // Assert
        response.ShouldNotBeNull();
        response.Result.ShouldBe(serviceResult);
        response.Result.IsSuccess.ShouldBeTrue();
        _payrollService.Verify(s => s.GetPayrollByIdAsync(request.Id), Times.Once);
    }

    [Fact]
    public async Task Handle_ShouldReturnEmpty_WhenNoLeaveFormFound()
    {
        // Arrange 
        var request = new GetPayrollByIdQueryRequest(Id: Guid.NewGuid());

        var serviceResult =
            ServiceResult<GetPayrollByIdDto>.Failure(PayrollConstant.NotFound, HttpStatusCode.InternalServerError);
        _payrollService
            .Setup(s => s.GetPayrollByIdAsync(request.Id))
            .ReturnsAsync(serviceResult);

        // Act 
        var response = await _handler.Handle(request, default);

        // Assert
        response.ShouldNotBeNull();
        response.Result.ShouldBe(serviceResult);
        response.Result.ErrorMessages.ShouldNotBeNull();
        _payrollService.Verify(s => s.GetPayrollByIdAsync(request.Id), Times.Once);
    }

    private GetPayrollByIdDto GeneratePayrollDto()
    {
        return new GetPayrollByIdDto(
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
        );
    }
}