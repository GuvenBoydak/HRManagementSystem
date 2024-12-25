namespace HrManagement.Application.Features.Payroll.Queries.GetPayrollById;

public record GetPayrollByIdQueryRequest(Guid Id):IQuery<GetPayrollByIdQueryResponse>;