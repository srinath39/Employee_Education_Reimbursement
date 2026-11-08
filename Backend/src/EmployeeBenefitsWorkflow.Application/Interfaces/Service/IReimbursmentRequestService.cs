using EmployeeBenefitsWorkflow.Application.DTOs;
using EmployeeBenefitsWorkflow.Domain.Entities;

namespace EmployeeBenefitsWorkflow.Application.interfaces.Service;

public interface IReimbursmentRequestService
{
    public ResponseEmployeeStatistics GetEmployeeStatistics(int employeeId);

    public List<ResponseReimbursmentDto> GetAllRequestOfEmployee(int employeeId);

    public ResponseReimbursmentDto GetRequestById(int RequestId);

    public ResponseReimbursmentDto CreateRequest(RequestReimbursmentDto requestDto);


    public ResponseHrStatistics GetHrStatistics(int hrId);

    public List<ResponseReimbursmentDto> GetAllPendingRequestOfHr();

    public void ApproveRequest(int RequestId);

    public void RejectRequest(int RequestId);
}   