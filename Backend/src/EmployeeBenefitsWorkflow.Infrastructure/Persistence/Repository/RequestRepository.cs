

using EmployeeBenefitsWorkflow.Application.interfaces.Repository;
using EmployeeBenefitsWorkflow.Domain.Entities;
using EmployeeBenefitsWorkflow.Domain.Enums;

namespace EmployeeBenefitsWorkflow.Infrastructure.Persistence.Repository;

public class RequestRepository : IRequestRepository
{

    private readonly ApplicationDBContext _db;

    public RequestRepository(ApplicationDBContext dBContext)
    {
        _db=dBContext;
    }

    public Guid CreateRequest(ReimbursementRequest request)
    {
        _db.Requests.Add(request);
        SaveRequestChanges();
        return request.RequestId;
    }

    public List<ReimbursementRequest> GetAllRequestByEmployeeId(Guid empId)
    {
        return _db.Sp_getAllRequests().Where(request=>request.EmployeeId.Equals(empId)).ToList();
    }

    public List<ReimbursementRequest> GetAllRequestByRequestStatus(EnumRequestStatus status)
    {
        return _db.Requests.Where(request=>request.RequestStatus.Equals(status)).ToList();
    }

    public ReimbursementRequest GetRequestByRequestId(Guid requestId)
    {
        return _db.Requests.FirstOrDefault(request=>request.RequestId.Equals(requestId))!;
    }

    public void SaveRequestChanges()
    {
        _db.SaveChanges();
    }
}