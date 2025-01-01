using System.Net;
using HrManagement.Application;
using HrManagement.Application.Features.Employee.Commands.Update;
using HrManagement.Application.Interfaces.Services;
using HrManagement.Domain.Enums;
using Moq;
using Shouldly;

namespace HManagement.UnitTest.Features.Employee.Commands;

public class UpdateEmployeeCommandUnitTest
{
    private readonly Mock<IEmployeeService> _employeeService;
    private readonly UpdateEmployeeCommandHandler _handler;

    public UpdateEmployeeCommandUnitTest()
    {
        _employeeService = new Mock<IEmployeeService>();
        _handler = new UpdateEmployeeCommandHandler(_employeeService.Object);
    }
    
    [Fact]
    public async Task Handle_ShouldReturnSuccessResponse_WhenEmployeeIsUpdatedSuccessfully()
    {
        var request = CreateUpdateEmployeeCommandRequest();
        
        var serviceResult = ServiceResult.Success();
        _employeeService
            .Setup(s => s.UpdateAsync(request))
            .ReturnsAsync(serviceResult);
        
        var handler = new UpdateEmployeeCommandHandler(_employeeService.Object);

        UpdateEmployeeCommandResponse response = await handler.Handle(request, default);
        
        response.ShouldNotBeNull();
        response.Response.ShouldBe(serviceResult);
        response.Response.IsSuccess.ShouldBeTrue();
        _employeeService.Verify(s => s.UpdateAsync(request), Times.Once);
    }
    
    [Fact]
    public async Task Handle_ShouldReturnFailureResponse_WhenEmployeeUpdateFails()
    {
        var request = CreateUpdateEmployeeCommandRequest();
        
        var serviceResult = ServiceResult.Failure("Failed to update employee", HttpStatusCode.InternalServerError);
        _employeeService
            .Setup(s => s.UpdateAsync(request))
            .ReturnsAsync(serviceResult);
        
        var handler = new UpdateEmployeeCommandHandler(_employeeService.Object);

        UpdateEmployeeCommandResponse response = await handler.Handle(request, default);
        
        response.ShouldNotBeNull();
        response.Response.ShouldBe(serviceResult);
        response.Response.ErrorMessages.ShouldNotBeNull();
        _employeeService.Verify(s => s.UpdateAsync(request), Times.Once);
    }

    private UpdateEmployeeCommandRequest CreateUpdateEmployeeCommandRequest()
    {
       return new UpdateEmployeeCommandRequest(
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
            ManagerId: Guid.NewGuid()
        );
    }
}