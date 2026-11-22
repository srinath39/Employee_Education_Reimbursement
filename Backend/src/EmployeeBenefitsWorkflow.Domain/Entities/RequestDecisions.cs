using System.ComponentModel.DataAnnotations;
using EmployeeBenefitsWorkflow.Domain.Enums;

namespace EmployeeBenefitsWorkflow.Domain.Entities;

public class RequestDecisions
{
    [Key]
    public Guid DecisionId { get;  set; }

    [Required]
    public Guid RequestId { get;  set; }

    [Required]
    public ReimbursementRequest Request { get;  set; } = null!;

    [Required]
    public Guid HrId { get;  set; }

    [Required]
    public User Hr { get;  set; } = null!;

    [Required]
    public EnumRequestStatus RequestStatus { get;  set; }

    public RequestDecisions()
    {
    }

    public RequestDecisions(ReimbursementRequest request, User user, EnumRequestStatus status)
    {
        Request = request;
        Hr = user;
        RequestStatus = status;
    }

}