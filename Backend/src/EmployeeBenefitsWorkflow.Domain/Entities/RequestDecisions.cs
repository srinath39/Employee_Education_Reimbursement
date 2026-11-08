using EmployeeBenefitsWorkflow.Domain.Enums;

namespace EmployeeBenefitsWorkflow.Domain.Entities;

public class RequestDecisions
{
    public int DecisionId { get; private set; }

    public ReimbursementRequest Request { get; private set; }

    public User Hr { get; private set; } 

    public EnumRequestStatus RequestStatus { get; private set; } 

    public RequestDecisions(int id, ReimbursementRequest request, User user, EnumRequestStatus status)
    {
        DecisionId = id;
        Request = request;
        Hr = user;
        RequestStatus = status;
    }

}