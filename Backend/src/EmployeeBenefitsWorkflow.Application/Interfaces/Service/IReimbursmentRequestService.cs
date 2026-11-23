using EmployeeBenefitsWorkflow.Application.DTOs;
using EmployeeBenefitsWorkflow.Domain.Entities;

namespace EmployeeBenefitsWorkflow.Application.interfaces.Service;

public interface IReimbursmentRequestService
{
    public Task<ResponseEmployeeStatistics> GetEmployeeStatistics(Guid employeeId);

    public Task<List<ResponseReimbursmentDto>> GetAllRequestOfEmployee(Guid employeeId);

    public Task<ResponseReimbursmentDto> GetRequestById(Guid RequestId);

    public Task<ResponseReimbursmentDto> CreateRequest(Guid empId, RequestReimbursmentDto requestDto);


    public Task<ResponseHrStatistics> GetHrStatistics(Guid hrId);

    public Task<List<ResponseReimbursmentDto>> GetAllPendingRequestOfHr();

    public Task ApproveRequest(Guid hrId, Guid RequestId);

    public Task RejectRequest(Guid hrId, Guid RequestId);
}