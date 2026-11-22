
using EmployeeBenefitsWorkflow.Domain.Entities;
using EmployeeBenefitsWorkflow.Domain.Enums;

namespace EmployeeBenefitsWorkflow.Application.interfaces.Repository;

public interface IRequestRepository
{
    public Guid CreateRequest(ReimbursementRequest request);

    public void SaveRequestChanges();

    public List<ReimbursementRequest> GetAllRequestByEmployeeId(Guid empId);

    public ReimbursementRequest GetRequestByRequestId(Guid requestId);

    public List<ReimbursementRequest> GetAllRequestByRequestStatus(EnumRequestStatus status);

}