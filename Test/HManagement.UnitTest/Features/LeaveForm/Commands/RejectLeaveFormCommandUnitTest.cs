using System.Net;
using HrManagement.Application;
using HrManagement.Application.Features.LeaveForm.Commands.RejectLeaveFormStatus;
using HrManagement.Application.Interfaces.Services;
using Moq;
using Shouldly;

namespace HManagement.UnitTest.Features.LeaveForm.Commands;

public class RejectLeaveFormCommandUnitTest
{
    private readonly Mock<ILeaveFormService> _leaveFormService;
    private readonly RejectLeaveFormStatusCommandHandler _handler;

    public RejectLeaveFormCommandUnitTest()
    {
        _leaveFormService = new Mock<ILeaveFormService>();
        _handler = new RejectLeaveFormStatusCommandHandler(_leaveFormService.Object);
    }

    [Fact]
    public async Task Handle_ShouldReturnSuccessResponse_WhenRejectLeaveFormSuccessfully()
    {
        // Arrange
        var request = new RejectLeaveFormStatusCommandRequest(
            Id: Guid.NewGuid(),
            RejectedId: Guid.NewGuid(),
            Reason: "I'm sorry, but your leave form is rejected.");

        var serviceResult = ServiceResult.Success(HttpStatusCode.NoContent);
        _leaveFormService
            .Setup(s => s.RejectLeaveFormStatus(request))
            .ReturnsAsync(serviceResult);

        // Act  
        var response = await _handler.Handle(request, default);

        // Assert
        response.ShouldNotBeNull();
        response.Result.ShouldBe(serviceResult);
        response.Result.IsSuccess.ShouldBeTrue();
        _leaveFormService.Verify(s => s.RejectLeaveFormStatus(request), Times.Once);
    }

    [Fact]
    public async Task Handle_ShouldReturnFailureResponse_WhenRejectLeaveFormFails()
    {
        // Arrange
        var request = new RejectLeaveFormStatusCommandRequest(
            Id: Guid.NewGuid(),
            RejectedId: Guid.NewGuid(),
            Reason: "I'm sorry, but your leave form is rejected.");

        var serviceResult = ServiceResult.Failure("Failed to reject leave form", HttpStatusCode.InternalServerError);
        _leaveFormService
            .Setup(s => s.RejectLeaveFormStatus(request))
            .ReturnsAsync(serviceResult);

        // Act
        var response = await _handler.Handle(request, default);

        // Assert
        response.ShouldNotBeNull();
        response.Result.ShouldBe(serviceResult);
        response.Result.ErrorMessages.ShouldNotBeNull();
        _leaveFormService.Verify(s => s.RejectLeaveFormStatus(request), Times.Once);
    }
}