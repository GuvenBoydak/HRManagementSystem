using HrManagement.Application;
using HrManagement.Application.Features.Employee.Dtos;
using HrManagement.Application.Features.Performance.Queries.GetAllPerformanceWithEmployeeId;
using HrManagement.Application.Interfaces.Services;
using HrManagement.Domain.Enums;
using Moq;
using Shouldly;

namespace HManagement.UnitTest.Features.Performance.Queries;

public class GetAllPerformanceWithEmployeeIdQueryUnitTest
{
    private readonly Mock<IPerformanceService> _performanceService;
    private readonly GetAllPerformanceWithEmployeeIdQueryHandler _handler;

    public GetAllPerformanceWithEmployeeIdQueryUnitTest()
    {
        _performanceService = new Mock<IPerformanceService>();
        _handler = new GetAllPerformanceWithEmployeeIdQueryHandler(_performanceService.Object);
    }

    [Fact]
    public async Task Handle_ShouldReturnSuccessResponse_WhenPerformanceAreRetrievedSuccessfully()
    {
        // Arrange
        var request = new GetAllPerformanceWithEmployeeIdQueryRequest(
            EmployeeId: Guid.NewGuid());
        var performanceDtos = GeneratePerformanceDtos();

        var serviceResult = ServiceResult<List<GetAllPerformanceWithEmployeeIdDto>>.Success(performanceDtos);
        _performanceService
            .Setup(s => s.GetAllPerformanceWithEmployeeIdAsync(request.EmployeeId))
            .ReturnsAsync(serviceResult);

        // Act
        var response = await _handler.Handle(request, default);

        // Assert
        response.ShouldNotBeNull();
        response.Result.ShouldBe(serviceResult);
        response.Result.IsSuccess.ShouldBeTrue();
        _performanceService.Verify(s => s.GetAllPerformanceWithEmployeeIdAsync(request.EmployeeId), Times.Once);
    }

    [Fact]
    public async Task Handle_ShouldReturnEmptyList_WhenNoPerformanceAreFound()
    {
        // Arrange
        var request = new GetAllPerformanceWithEmployeeIdQueryRequest(
            EmployeeId: Guid.NewGuid());
        var performanceDtos = new List<GetAllPerformanceWithEmployeeIdDto>();

        var serviceResult = ServiceResult<List<GetAllPerformanceWithEmployeeIdDto>>.Success(performanceDtos);
        _performanceService
            .Setup(s => s.GetAllPerformanceWithEmployeeIdAsync(request.EmployeeId))
            .ReturnsAsync(serviceResult);

        // Act
        var response = await _handler.Handle(request, default);

        // Assert
        response.ShouldNotBeNull();
        response.Result.ShouldBe(serviceResult);
        response.Result.Data.ShouldBeEmpty();
        _performanceService.Verify(s => s.GetAllPerformanceWithEmployeeIdAsync(request.EmployeeId), Times.Once);
    }

    private List<GetAllPerformanceWithEmployeeIdDto> GeneratePerformanceDtos()
    {
        return new List<GetAllPerformanceWithEmployeeIdDto>
        {
            new GetAllPerformanceWithEmployeeIdDto(
                Id: Guid.NewGuid(),
                FeedBack: "Excellent work",
                WorkPerformanceScore: 9,
                TeamworkScore: 8,
                CommunicationScore: 8,
                LeadershipScore: 8,
                OverallScore: 8,
                ReviewStartDate: new DateTime(2023, 1, 1),
                ReviewEndDate: new DateTime(2023, 6, 30),
                ReviewedUserId: Guid.NewGuid(),
                EmployeeId: Guid.NewGuid(),
                CreatedDate: DateTime.UtcNow,
                UpdatedDate: DateTime.UtcNow,
                DeletedDate: DateTime.MinValue,
                IsDeleted: false,
                Employee: new EmployeeDto(
                    Id: Guid.NewGuid(),
                    Name: "Jane",
                    Surname: "Doe",
                    Email: "jane.doe@example.com",
                    Phone: "123-456-7890",
                    Gender: EmployeeGender.Female,
                    DateOfBirth: new DateTime(1992, 5, 15),
                    Address: "456 Elm St",
                    Position: "Manager",
                    Department: EmployeeDepartment.Employee,
                    Salary: 80000m,
                    HireDate: new DateTime(2018, 3, 1),
                    PerformanceScore: 9,
                    LeaveUsed: 2,
                    CreatedDate: DateTime.UtcNow,
                    UpdatedDate: DateTime.UtcNow,
                    DeletedDate: DateTime.MinValue,
                    IsDeleted: false)
            ),
            new GetAllPerformanceWithEmployeeIdDto(
                Id: Guid.NewGuid(),
                FeedBack: "Excellent work",
                WorkPerformanceScore: 9,
                TeamworkScore: 8,
                CommunicationScore: 8,
                LeadershipScore: 8,
                OverallScore: 8,
                ReviewStartDate: new DateTime(2023, 1, 1),
                ReviewEndDate: new DateTime(2023, 6, 30),
                ReviewedUserId: Guid.NewGuid(),
                EmployeeId: Guid.NewGuid(),
                CreatedDate: DateTime.UtcNow,
                UpdatedDate: DateTime.UtcNow,
                DeletedDate: DateTime.MinValue,
                IsDeleted: false,
                Employee: new EmployeeDto(
                    Id: Guid.NewGuid(),
                    Name: "Jane",
                    Surname: "Doe",
                    Email: "jane.doe@example.com",
                    Phone: "123-456-7890",
                    Gender: EmployeeGender.Female,
                    DateOfBirth: new DateTime(1992, 5, 15),
                    Address: "456 Elm St",
                    Position: "Manager",
                    Department: EmployeeDepartment.Employee,
                    Salary: 80000m,
                    HireDate: new DateTime(2018, 3, 1),
                    PerformanceScore: 9,
                    LeaveUsed: 2,
                    CreatedDate: DateTime.UtcNow,
                    UpdatedDate: DateTime.UtcNow,
                    DeletedDate: DateTime.MinValue,
                    IsDeleted: false)
            )
        };
    }
}