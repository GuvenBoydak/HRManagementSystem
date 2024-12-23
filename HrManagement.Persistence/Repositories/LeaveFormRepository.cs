using HrManagement.Application.Interfaces.Repositories;
using HrManagement.Domain.Entities;
using HrManagement.Persistence.Contexts;

namespace HrManagement.Persistence.Repositories;

public class LeaveFormRepository(AppDbContext context):GenericRepository<LeaveForm>(context), ILeaveFormRepository
{
    
}