using EmployeeBenefitsWorkflow.Application.DTOs;
using EmployeeBenefitsWorkflow.Domain.Entities;

namespace EmployeeBenefitsWorkflow.Application.interfaces.Service;

public interface IReimbursmentRequestService
{
    public ResponseEmployeeStatistics GetEmployeeStatistics(Guid employeeId);

    public List<ResponseReimbursmentDto> GetAllRequestOfEmployee(Guid employeeId);

    public ResponseReimbursmentDto GetRequestById(int RequestId);

    public ResponseReimbursmentDto CreateRequest(RequestReimbursmentDto requestDto);


    public ResponseHrStatistics GetHrStatistics(Guid hrId);

    public List<ResponseReimbursmentDto> GetAllPendingRequestOfHr();

    public void ApproveRequest(int RequestId);

    public void RejectRequest(int RequestId);
}   