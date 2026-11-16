using System.ComponentModel.DataAnnotations;
using EmployeeBenefitsWorkflow.Domain.Enums;

namespace EmployeeBenefitsWorkflow.Domain.Entities;

public class RequestDecisions
{
    [Key]
    public Guid DecisionId { get; private set; }

    [Required]
    public Guid RequestId {get;private set;}

    [Required]
    public ReimbursementRequest Request { get; private set; } = null!;

    [Required]
    public Guid HrId {get;private set;}

    [Required]
    public User Hr { get; private set; } = null!;

    [Required]
    public EnumRequestStatus RequestStatus { get; private set; } 

    public RequestDecisions(ReimbursementRequest request, User user, EnumRequestStatus status)
    {
        DecisionId = Guid.NewGuid();
        Request = request;
        Hr = user;
        RequestStatus = status;
    }

}