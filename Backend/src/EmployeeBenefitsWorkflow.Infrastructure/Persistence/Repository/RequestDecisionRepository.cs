

using EmployeeBenefitsWorkflow.Application.interfaces.Repository;
using EmployeeBenefitsWorkflow.Domain.Entities;
using EmployeeBenefitsWorkflow.Domain.Enums;

namespace EmployeeBenefitsWorkflow.Infrastructure.Persistence.Repository;

public class RequestDecisionRepository : IRequestDecisionRepository
{

    private readonly ApplicationDBContext _db;

    public RequestDecisionRepository(ApplicationDBContext dbContext)
    {
        _db=dbContext;
    }

    public void CreateRequestDecision(RequestDecisions requestDecision)
    {
        _db.RequestDecisions.Add(requestDecision);
    }

    public List<RequestDecisions> GetAllRequestDecisionsByRequestStatus(EnumRequestStatus status)
    {
        return _db.RequestDecisions.Where(requestDecision => requestDecision.RequestStatus.Equals(status)).ToList();
    }

    public List<RequestDecisions> GetRequestDecisionByHrId(Guid hrId)
    {
        return _db.RequestDecisions.Where(requestDecision => requestDecision.HrId.Equals(hrId)).ToList();
    }

    public void SaveRequestDecisionsChanges()
    {
        _db.SaveChanges();
    }
}