

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

    public async Task<Guid> CreateRequest(ReimbursementRequest request)
    {
        await _db.Requests.AddAsync(request);
        await SaveRequestChanges();
        return request.RequestId;
    }

    public async Task<List<ReimbursementRequest>> GetAllRequestByEmployeeId(Guid empId)
    {
        var requests = await _db.Sp_getAllRequests()
        .AsAsyncEnumerable()
        .Where(r => r.EmployeeId == empId)
        .ToListAsync();

        foreach (var request in requests)
        {
            _db.Entry(request)
                .Reference(r => r.Employee)
                .Load();
        }

        return requests;
    }

    public async Task<List<ReimbursementRequest>> GetAllRequestByRequestStatus(EnumRequestStatus status)
    {
        return await _db.Requests.Include(r => r.Employee).Where(request => request.RequestStatus.Equals(status)).ToListAsync();
    }

    public async Task<ReimbursementRequest> GetRequestByRequestId(Guid requestId)
    {
        ReimbursementRequest? request = await _db.Requests.Include(r => r.Employee).FirstOrDefaultAsync(request => request.RequestId.Equals(requestId))!;
        return request!;
    }

    public async Task SaveRequestChanges()
    {
       await _db.SaveChangesAsync();
    }
}