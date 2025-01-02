using System.Net;
using HrManagement.Application;
using HrManagement.Application.Features.Employee.Commands.Delete;
using HrManagement.Application.Interfaces.Services;
using Moq;
using Shouldly;

namespace HManagement.UnitTest.Features.Employee.Commands;

public class DeleteEmployeeCommandUnitTest
{
    private readonly Mock<IEmployeeService> _employeeService;
    private readonly DeleteEmployeeCommandHandler _handler;

    public DeleteEmployeeCommandUnitTest()
    {
        _employeeService = new Mock<IEmployeeService>();
        _handler = new DeleteEmployeeCommandHandler(_employeeService.Object);
    }

    [Fact]
    public async Task Handle_ShouldReturnSuccessResponse_WhenEmployeeIsDeletedSuccessfully()
    {
        // Arrange
        var request = new DeleteEmployeeCommandRequest(Id:Guid.NewGuid());
        
        var serviceResult = ServiceResult.Success(HttpStatusCode.NoContent);

        _employeeService
            .Setup(s => s.DeleteAsync(request))
            .ReturnsAsync(serviceResult);
        
        // Act
        var response = await _handler.Handle(request, default);

        // Assert
        response.ShouldNotBeNull();
        response.Response.ShouldBe(serviceResult);
        response.Response.IsSuccess.ShouldBeTrue();
        _employeeService.Verify(s => s.DeleteAsync(request), Times.Once);
    }
    
    [Fact]
    public async Task Handle_ShouldReturnFailureResponse_WhenEmployeeIsDeleteFails()
    {
        // Arrange
        var request = new DeleteEmployeeCommandRequest(Id:Guid.NewGuid());
        
        var serviceResult = ServiceResult.Failure("Failed to delete employee", HttpStatusCode.InternalServerError);

        _employeeService
            .Setup(s => s.DeleteAsync(request))
            .ReturnsAsync(serviceResult);
        
        // Act
        var response = await _handler.Handle(request, default);

        // Assert
        response.ShouldNotBeNull();
        response.Response.ShouldBe(serviceResult);
        response.Response.ErrorMessages.ShouldNotBeNull();
        _employeeService.Verify(s => s.DeleteAsync(request), Times.Once);
    }
    
}