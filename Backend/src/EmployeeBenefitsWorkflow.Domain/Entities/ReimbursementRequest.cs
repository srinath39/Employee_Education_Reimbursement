using EmployeeBenefitsWorkflow.Domain.Enums;

namespace EmployeeBenefitsWorkflow.Domain.Entities;

public class ReimbursementRequest
{
    public int RequestId { get; private set; }

    public User? Employee { get; private set; }

    public string? CourseName { get; private set; }

    public double ClaimAmount { get; private set; }

    public EnumRequestStatus RequestStatus { get; private set; } = EnumRequestStatus.UNDER_REVIEW;

    public List<RequestDocuments>? RequestDocuments { get; private set; }

    public ReimbursementRequest(int id, string courseName,double claimAmount)
    {
        RequestId=id;
        CourseName=courseName;
        ClaimAmount=claimAmount;
    }

    public ReimbursementRequest(int id, User employee, string courseName, double claimAmount, EnumRequestStatus status, List<RequestDocuments> requestDocuments)
    {
        RequestId = id;
        Employee = employee;
        CourseName = courseName;
        ClaimAmount = claimAmount;
        RequestStatus = status;
        RequestDocuments = requestDocuments;
    }

    public void ApproveRequest()
    {
        if (this.RequestStatus != EnumRequestStatus.UNDER_REVIEW)
        {
            new InvalidOperationException("Only Under Review request are allowed to be approve");
        }
        RequestStatus = EnumRequestStatus.APPROVED;
    }

    public void RejectRequest()
    {
        if (this.RequestStatus != EnumRequestStatus.UNDER_REVIEW)
        {
            new InvalidOperationException("Only Under Review request are allowed to be reject");
        }
        RequestStatus = EnumRequestStatus.REJECTED;
    }
}