using System.Net;
using HrManagement.Application;
using HrManagement.Application.Features.Payroll.Commands.Update;
using HrManagement.Application.Interfaces.Services;
using Moq;
using Shouldly;

namespace HManagement.UnitTest.Features.Payroll.Commands;

public class UpdatePayrollCommandUnitTest
{
    private readonly Mock<IPayrollService> _payrollService;
    private readonly UpdatePayrollCommandHandler _handler;

    public UpdatePayrollCommandUnitTest()
    {
        _payrollService = new Mock<IPayrollService>();
        _handler = new UpdatePayrollCommandHandler(_payrollService.Object);
    }

    [Fact]
    public async Task Handle_ShouldReturnSuccessResponse_WhenLeaveFormIsUpdatededSuccessfully()
    {
        // Arrange
        var request = CreatePayrollRequest();

        var serviceResult = ServiceResult.Success(HttpStatusCode.NoContent);
        _payrollService.Setup(s => s.UpdateAsync(request))
            .ReturnsAsync(serviceResult);

        // Act
        var response = await _handler.Handle(request, default);

        // Assert
        response.ShouldNotBeNull();
        response.Result.ShouldBe(serviceResult);
        response.Result.IsSuccess.ShouldBeTrue();
        _payrollService.Verify(s => s.UpdateAsync(request), Times.Once);
    }

    [Fact]
    public async Task Handle_ShouldReturnFailureResponse_WhenLeaveFormUpdateFails()
    {
        // Arrange
        var request = CreatePayrollRequest();

        var serviceResult = ServiceResult.Failure("Failed to update payroll", HttpStatusCode.InternalServerError);
        _payrollService.Setup(s => s.UpdateAsync(request))
            .ReturnsAsync(serviceResult);

        // Act
        var response = await _handler.Handle(request, default);

        // Assert
        response.ShouldNotBeNull();
        response.Result.ShouldBe(serviceResult);
        response.Result.ErrorMessages.ShouldNotBeNull();
        _payrollService.Verify(s => s.UpdateAsync(request), Times.Once);
    }

    private UpdatePayrollCommandRequest CreatePayrollRequest()
    {
        return new UpdatePayrollCommandRequest(
            Id: Guid.NewGuid(),
            BasicSalary: 15000.00m,
            Allowances: 1000.00m,
            Overtime: 200.00m,
            Deductions: 150.00m,
            Tax: 500.00m,
            PaymentDate: new DateTime(2023, 10, 1),
            BankAccountNumber: "1234567890",
            Comments: "Monthly payroll",
            RetirementFund: 300.00m);
    }
}