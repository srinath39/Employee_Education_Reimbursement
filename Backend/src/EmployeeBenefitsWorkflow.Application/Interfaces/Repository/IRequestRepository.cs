
using EmployeeBenefitsWorkflow.Domain.Entities;
using EmployeeBenefitsWorkflow.Domain.Enums;

namespace EmployeeBenefitsWorkflow.Application.interfaces.Repository;

public interface IRequestRepository
{
    public Task<Guid> CreateRequest(ReimbursementRequest request);

    public Task SaveRequestChanges();

    public Task<List<ReimbursementRequest>> GetAllRequestByEmployeeId(Guid empId);

    public Task<ReimbursementRequest> GetRequestByRequestId(Guid requestId);

    public Task<List<ReimbursementRequest>> GetAllRequestByRequestStatus(EnumRequestStatus status);

}