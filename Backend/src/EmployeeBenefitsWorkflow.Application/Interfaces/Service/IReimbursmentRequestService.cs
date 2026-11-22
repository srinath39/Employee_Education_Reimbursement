using EmployeeBenefitsWorkflow.Application.DTOs;
using EmployeeBenefitsWorkflow.Domain.Entities;

namespace EmployeeBenefitsWorkflow.Application.interfaces.Service;

public interface IReimbursmentRequestService
{
    public ResponseEmployeeStatistics GetEmployeeStatistics(Guid employeeId);

    public List<ResponseReimbursmentDto> GetAllRequestOfEmployee(Guid employeeId);

    public ResponseReimbursmentDto GetRequestById(Guid RequestId);

    public ResponseReimbursmentDto CreateRequest(Guid empId, RequestReimbursmentDto requestDto);


    public ResponseHrStatistics GetHrStatistics(Guid hrId);

    public List<ResponseReimbursmentDto> GetAllPendingRequestOfHr();

    public void ApproveRequest(Guid hrId, Guid RequestId);

    public void RejectRequest(Guid hrId, Guid RequestId);
}