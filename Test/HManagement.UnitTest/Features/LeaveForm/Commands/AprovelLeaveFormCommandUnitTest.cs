using System.Net;
using HrManagement.Application;
using HrManagement.Application.Features.LeaveForm.Commands.AproveLeaveFormStatus;
using HrManagement.Application.Interfaces.Services;
using Moq;
using Shouldly;

namespace HManagement.UnitTest.Features.LeaveForm.Commands;

public class AprovelLeaveFormCommandUnitTest
{
    private readonly Mock<ILeaveFormService> _leaveFormService;
    private readonly AproveLeaveFormStatusCommandHandler _handler;

    public AprovelLeaveFormCommandUnitTest()
    {
        _leaveFormService = new Mock<ILeaveFormService>();
        _handler = new AproveLeaveFormStatusCommandHandler(_leaveFormService.Object);
    }

    [Fact]
    public async Task Handle_ShouldReturnSuccessResponse_WhenAprovelLeaveFormSuccessfully()
    {
        // Arrange
        var request = new AproveLeaveFromStatusCommandRequest(
            Id: Guid.NewGuid(),
            ApprovedId: Guid.NewGuid());

        var serviceResult = ServiceResult.Success(HttpStatusCode.NoContent);
        _leaveFormService
            .Setup(s => s.ApproveLeaveFormStatus(request))
            .ReturnsAsync(serviceResult);

        // Act
        var response = await _handler.Handle(request, default);

        // Assert
        response.ShouldNotBeNull();
        response.Result.ShouldBe(serviceResult);
        response.Result.IsSuccess.ShouldBeTrue();
        _leaveFormService.Verify(s => s.ApproveLeaveFormStatus(request), Times.Once);
    }

    [Fact]
    public async Task Handle_ShouldReturnFailureResponse_WhenAprovelLeaveFormFails()
    {
        // Arrange
        var request = new AproveLeaveFromStatusCommandRequest(
            Id: Guid.NewGuid(),
            ApprovedId: Guid.NewGuid());

        var serviceResult = ServiceResult.Failure("Failed to approve leave form", HttpStatusCode.InternalServerError);
        _leaveFormService
            .Setup(s => s.ApproveLeaveFormStatus(request))
            .ReturnsAsync(serviceResult);

        // Act
        var response = await _handler.Handle(request, default);

        // Assert
        response.ShouldNotBeNull();
        response.Result.ShouldBe(serviceResult);
        response.Result.ErrorMessages.ShouldNotBeNull();
        _leaveFormService.Verify(s => s.ApproveLeaveFormStatus(request), Times.Once);
    }
}