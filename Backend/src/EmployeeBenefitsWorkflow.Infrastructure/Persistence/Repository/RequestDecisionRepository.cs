

using EmployeeBenefitsWorkflow.Application.interfaces.Repository;
using EmployeeBenefitsWorkflow.Domain.Entities;
using EmployeeBenefitsWorkflow.Domain.Enums;
using Microsoft.EntityFrameworkCore;

namespace EmployeeBenefitsWorkflow.Infrastructure.Persistence.Repository;

public class RequestDecisionRepository : IRequestDecisionRepository
{

    private readonly ApplicationDBContext _db;

    public RequestDecisionRepository(ApplicationDBContext dbContext)
    {
        _db=dbContext;
    }

    public async Task CreateRequestDecision(RequestDecisions requestDecision)
    {
        await _db.RequestDecisions.AddAsync(requestDecision);
    }

    public async Task<List<RequestDecisions>> GetAllRequestDecisionsByRequestStatus(EnumRequestStatus status)
    {
        return await _db.RequestDecisions.Include(rd=>rd.Request).Include(rd=>rd.Hr).Where(requestDecision => requestDecision.RequestStatus.Equals(status)).ToListAsync();
    }

    public async Task<List<RequestDecisions>> GetRequestDecisionByHrId(Guid hrId)
    {
        return await _db.RequestDecisions.Include(rd=>rd.Request).Include(rd=>rd.Hr).Where(requestDecision => requestDecision.HrId.Equals(hrId)).ToListAsync();
    }

    public async Task SaveRequestDecisionsChanges()
    {
        await _db.SaveChangesAsync();
    }
}