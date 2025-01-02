using System.Net;
using HrManagement.Application;
using HrManagement.Application.Features.Performance.Commands.Delete;
using HrManagement.Application.Interfaces.Services;
using Moq;
using Shouldly;

namespace HManagement.UnitTest.Features.Performance.Commands;

public class DeletePerformanceCommandUnitTest
{
    private readonly Mock<IPerformanceService> _performanceService;
    private readonly DeletePerformanceCommandHandler _handler;

    public DeletePerformanceCommandUnitTest()
    {
        _performanceService = new Mock<IPerformanceService>();
        _handler = new DeletePerformanceCommandHandler(_performanceService.Object);
    }

    [Fact]
    public async Task Handle_ShouldReturnSuccessResponse_WhenPerformanceIsDeletedSuccessfully()
    {
        // Arrange
        var request = new DeletePerformanceCommandRequest(Id: Guid.NewGuid());

        var serviceResult = ServiceResult.Success(HttpStatusCode.NoContent);
        _performanceService
            .Setup(s => s.DeleteAsync(request))
            .ReturnsAsync(serviceResult);

        // Act
        var response = await _handler.Handle(request, default);

        // Assert
        response.ShouldNotBeNull();
        response.Result.ShouldBe(serviceResult);
        response.Result.IsSuccess.ShouldBeTrue();
        _performanceService.Verify(s => s.DeleteAsync(request), Times.Once);
    }

    [Fact]
    public async Task Handle_ShouldReturnFailureResponse_WhenPerformanceDeleteFails()
    {
        // Arrange
        var request = new DeletePerformanceCommandRequest(Id: Guid.NewGuid());

        var serviceResult = ServiceResult.Failure("Failed to delete performance", HttpStatusCode.InternalServerError);
        _performanceService
            .Setup(s => s.DeleteAsync(request))
            .ReturnsAsync(serviceResult);

        // Act
        var response = await _handler.Handle(request, default);

        // Assert
        response.ShouldNotBeNull();
        response.Result.ShouldBe(serviceResult);
        response.Result.ErrorMessages.ShouldNotBeEmpty();
        _performanceService.Verify(s => s.DeleteAsync(request), Times.Once);
    }
}