using System.Net;
using HrManagement.Application;
using HrManagement.Application.Features.Employee.Commands.Create;
using HrManagement.Application.Interfaces.Services;
using HrManagement.Domain.Enums;
using Moq;
using Shouldly;

namespace HManagement.UnitTest.Features.Employee.Commands;

public class CreateEmployeeCommandUnitTest
{
    private readonly Mock<IEmployeeService> _employeeService;
    private readonly CreateEmployeeCommandHandler _handler;

    public CreateEmployeeCommandUnitTest()
    {
        _employeeService = new Mock<IEmployeeService>();
        _handler = new CreateEmployeeCommandHandler(_employeeService.Object);
    }
    
    [Fact]
    public async Task Handle_ShouldReturnSuccessResponse_WhenEmployeeIsCreatedSuccessfully()
    {
        // Arrange
        var request = CreateEmployeeCommandRequest();

        var employeeId = Guid.NewGuid();
        var serviceResult = ServiceResult<Guid>.SuccessAsCreated(employeeId, $"api/employees/{employeeId}");

        _employeeService
            .Setup(s => s.AddAsync(request, It.IsAny<CancellationToken>()))
            .ReturnsAsync(serviceResult);

        // Act
        var response = await _handler.Handle(request, default);

        // Assert
        response.ShouldNotBeNull();
        response.Response.ShouldBe(serviceResult);
        response.Response.Data.ShouldBe(employeeId);
        _employeeService.Verify(s => s.AddAsync(request, It.IsAny<CancellationToken>()), Times.Once);
    } 
    
    [Fact]
    public async Task Handle_ShouldReturnFailureResponse_WhenEmployeeCreationFails()
    {
        // Arrange
        var request = CreateEmployeeCommandRequest();

        var serviceResult = ServiceResult<Guid>.Failure("Failed to save employee", HttpStatusCode.InternalServerError);

        _employeeService
            .Setup(s => s.AddAsync(request, It.IsAny<CancellationToken>()))
            .ReturnsAsync(serviceResult);

        // Act
        var response = await _handler.Handle(request, default);

        // Assert
        response.ShouldNotBeNull();
        response.Response.ShouldBe(serviceResult);
        response.Response.ErrorMessages.ShouldNotBeNull();
        _employeeService.Verify(s => s.AddAsync(request, It.IsAny<CancellationToken>()), Times.Once);
    }

    private CreateEmployeeCommandRequest CreateEmployeeCommandRequest()
    {
        return new CreateEmployeeCommandRequest(
            Name: "John",
            Surname: "doe",
            Email: "john.doe@example.com",
            Phone: "123-456-7890",
            Gender: EmployeeGender.Male,
            DateOfBirth: new DateTime(1990, 1, 1),
            Address: "123 Main St",
            Position: "Developer",
            Department: EmployeeDepartment.Employee,
            Salary: 60000m,
            HireDate: new DateTime(2020, 1, 1));
    }
}