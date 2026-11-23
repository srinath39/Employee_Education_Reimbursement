

using EmployeeBenefitsWorkflow.Application.interfaces.Repository;
using EmployeeBenefitsWorkflow.Domain.Entities;
using EmployeeBenefitsWorkflow.Domain.Enums;
using Microsoft.EntityFrameworkCore;

namespace EmployeeBenefitsWorkflow.Infrastructure.Persistence.Repository;

public class RequestRepository : IRequestRepository
{

    private readonly ApplicationDBContext _db;

    public RequestRepository(ApplicationDBContext dBContext)
    {
        _db = dBContext;
    }

    public Guid CreateRequest(ReimbursementRequest request)
    {
        _db.Requests.Add(request);
        SaveRequestChanges();
        return request.RequestId;
    }

    public List<ReimbursementRequest> GetAllRequestByEmployeeId(Guid empId)
    {
        var requests = _db.Sp_getAllRequests()
        .AsEnumerable()
        .Where(r => r.EmployeeId == empId)
        .ToList();

        foreach (var request in requests)
        {
            _db.Entry(request)
                .Reference(r => r.Employee)
                .Load();
        }

        return requests;
    }

    public List<ReimbursementRequest> GetAllRequestByRequestStatus(EnumRequestStatus status)
    {
        return _db.Requests.Include(r => r.Employee).Where(request => request.RequestStatus.Equals(status)).ToList();
    }

    public ReimbursementRequest GetRequestByRequestId(Guid requestId)
    {
        return _db.Requests.Include(r => r.Employee).FirstOrDefault(request => request.RequestId.Equals(requestId))!;
    }

    public void SaveRequestChanges()
    {
        _db.SaveChanges();
    }
}