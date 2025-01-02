using HrManagement.Application;
using HrManagement.Application.Features.Employee.Dtos;
using HrManagement.Application.Features.LeaveForm.Queries.GetAllWithEmployeeId;
using HrManagement.Application.Interfaces.Services;
using HrManagement.Domain.Enums;
using Moq;
using Shouldly;

namespace HManagement.UnitTest.Features.LeaveForm.Queries;

public class GetAllLeaveFormWithEmployeeIdQueryUnitTest
{
    private readonly Mock<ILeaveFormService> _leaveFormService;
    private readonly GetAllWithEmployeeIdQueryHandler _handler;

    public GetAllLeaveFormWithEmployeeIdQueryUnitTest()
    {
        _leaveFormService = new Mock<ILeaveFormService>();
        _handler = new GetAllWithEmployeeIdQueryHandler(_leaveFormService.Object);
    }

    [Fact]
    public async Task Handle_ShouldReturnSuccessResponse_WhenLeaveFormsAreRetrievedSuccessfully()
    {
        // Arrange
        var request = new GetAllWithEmployeeIdQueryRequest(
            EmployeeId: Guid.NewGuid());
        var leaveFormList = new List<GetAllWithEmployeeIdDto>
        {
            CreateLeaveFormDto(),
            CreateLeaveFormDto()
        };

        var serviceResult = ServiceResult<List<GetAllWithEmployeeIdDto>>.Success(leaveFormList);
        _leaveFormService
            .Setup(s => s.GetAllWithEmployeeIdAsync(request.EmployeeId))
            .ReturnsAsync(serviceResult);

        // Act
        var response = await _handler.Handle(request, default);

        // Assert
        response.ShouldNotBeNull();
        response.Result.ShouldBe(serviceResult);
        response.Result.IsSuccess.ShouldBeTrue();
        _leaveFormService.Verify(s => s.GetAllWithEmployeeIdAsync(request.EmployeeId), Times.Once);
    }

    [Fact]
    public async Task Handle_ShouldReturnEmptyList_WhenNoLeaveFormsAreFound()
    {
        // Arrange
        var request = new GetAllWithEmployeeIdQueryRequest(
            EmployeeId: Guid.NewGuid());
        var leaveFormList = new List<GetAllWithEmployeeIdDto>();

        var serviceResult = ServiceResult<List<GetAllWithEmployeeIdDto>>.Success(leaveFormList);
        _leaveFormService
            .Setup(s => s.GetAllWithEmployeeIdAsync(request.EmployeeId))
            .ReturnsAsync(serviceResult);

        // Act 
        var response = await _handler.Handle(request, default);

        // Assert
        response.ShouldNotBeNull();
        response.Result.ShouldBe(serviceResult);
        response.Result.Data.ShouldBeEmpty();
        _leaveFormService.Verify(s => s.GetAllWithEmployeeIdAsync(request.EmployeeId), Times.Once);
    }

    private GetAllWithEmployeeIdDto CreateLeaveFormDto()
    {
        return new GetAllWithEmployeeIdDto(
            Id: Guid.NewGuid(),
            StartDate: new DateTime(2023, 1, 1),
            EndDate: new DateTime(2023, 1, 10),
            TotalDays: 10,
            Status: LeaveStatus.Pending,
            Reason: "Vacation",
            EmployeeId: Guid.NewGuid(),
            ApprovedUserId: Guid.NewGuid(),
            ApprovalDate: DateTime.Now,
            CreatedDate: DateTime.Now,
            UpdatedDate: DateTime.Now,
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