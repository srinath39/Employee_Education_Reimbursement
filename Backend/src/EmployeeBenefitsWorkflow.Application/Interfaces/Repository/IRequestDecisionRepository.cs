
using EmployeeBenefitsWorkflow.Domain.Entities;
using EmployeeBenefitsWorkflow.Domain.Enums;

namespace EmployeeBenefitsWorkflow.Application.interfaces.Repository;

public interface IRequestDecisionRepository
{
    public List<RequestDecisions> GetRequestDecisionByHrId(Guid hrId);

    public void SaveRequestDecisionsChanges();

    public List<RequestDecisions> GetAllRequestDecisionsByRequestStatus(EnumRequestStatus status);

    public void CreateRequestDecision(RequestDecisions requestDecision);

}