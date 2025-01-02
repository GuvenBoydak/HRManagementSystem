using System.Net;
using HrManagement.Application;
using HrManagement.Application.Features.Payroll.Commands.Create;
using HrManagement.Application.Interfaces.Services;
using Moq;
using Shouldly;

namespace HManagement.UnitTest.Features.Payroll.Commands;

public class CreatePayrollCommandUnitTest
{
    private readonly Mock<IPayrollService> _payrollService;
    private readonly CreatePayrollCommandHandler _handler;

    public CreatePayrollCommandUnitTest()
    {
        _payrollService = new Mock<IPayrollService>();
        _handler = new CreatePayrollCommandHandler(_payrollService.Object);
    }

    [Fact]
    public async Task Handle_ShouldReturnSuccessResponse_WhenPayrollIsCreatedSuccessfully()
    {
        // Arrange
        var request = CreatePayrollRequest();
        var payrollId = Guid.NewGuid();
        
        var serviceResult = ServiceResult<Guid>.SuccessAsCreated(payrollId, $"api/payroll/{payrollId}");
        _payrollService
            .Setup(s => s.AddAsync(request, It.IsAny<CancellationToken>()))
            .ReturnsAsync(serviceResult);
        
        // Act
        var response = await _handler.Handle(request, default);
        
        // Assert 
        response.ShouldNotBeNull();
        response.Result.ShouldBe(serviceResult);
        response.Result.IsSuccess.ShouldBeTrue();
        _payrollService.Verify(s=>s.AddAsync(request, It.IsAny<CancellationToken>()), Times.Once);
    }

    [Fact]
    public async Task Handle_ShouldReturnFailureResponse_WhenPayrollCreationFails()
    {
        // Arrange
        var request = CreatePayrollRequest();
        
        var serviceResult = ServiceResult<Guid>.Failure("Failed to save payroll", HttpStatusCode.InternalServerError);
        _payrollService
            .Setup(s => s.AddAsync(request, It.IsAny<CancellationToken>()))
            .ReturnsAsync(serviceResult);
        
        // Act
        var response = await _handler.Handle(request, default);
        
        // Assert 
        response.ShouldNotBeNull();
        response.Result.ShouldBe(serviceResult);
        response.Result.ErrorMessages.ShouldNotBeNull();
        _payrollService.Verify(s=>s.AddAsync(request, It.IsAny<CancellationToken>()), Times.Once);
    }

    private CreatePayrollCommandRequest CreatePayrollRequest()
    {
        return new CreatePayrollCommandRequest(
            BasicSalary: 15000.00m,
            Allowances: 1000.00m,
            Overtime: 200.00m,
            Deductions: 150.00m,
            Tax: 500.00m,
            PayrollDate: new DateTime(2023, 10, 1),
            BankAccountNumber: "1234567890",
            Comments: "Monthly payroll",
            RetirementFund: 300.00m,
            EmployeeId: Guid.NewGuid()
        );
    }
}