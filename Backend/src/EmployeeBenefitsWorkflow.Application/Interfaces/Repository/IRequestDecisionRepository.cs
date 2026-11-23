
using EmployeeBenefitsWorkflow.Domain.Entities;
using EmployeeBenefitsWorkflow.Domain.Enums;

namespace EmployeeBenefitsWorkflow.Application.interfaces.Repository;

public interface IRequestDecisionRepository
{
    public Task<List<RequestDecisions>> GetRequestDecisionByHrId(Guid hrId);

    public Task SaveRequestDecisionsChanges();

    public Task<List<RequestDecisions>> GetAllRequestDecisionsByRequestStatus(EnumRequestStatus status);

    public Task CreateRequestDecision(RequestDecisions requestDecision);

}