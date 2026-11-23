using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using EmployeeBenefitsWorkflow.Domain.Enums;

namespace EmployeeBenefitsWorkflow.Domain.Entities;

public class ReimbursementRequest
{
    [Key]
    public Guid RequestId { get;  set; }

    [Required]
    public Guid EmployeeId { get;  set; }    // Foreign key , automatically recognised by EF Core 

    [Required]
    public User Employee { get;  set; } = null!;   // navigation property usually this is not stored in database, but its id Employee Id is stored as a foreign key 
                                                            // this is useful for direct access 

    [Required]
    [StringLength(100)]
    public string CourseName { get;  set; } = null!;

    [Required]
    [Column(TypeName = "decimal(18,2)")]
    public decimal ClaimAmount { get;  set; }

    [Required]
    public EnumRequestStatus RequestStatus { get; set; }

    public RequestDecisions Decision {get;set;} =null!;


    // public List<RequestDocuments>? RequestDocuments { get; private set; }

    public ReimbursementRequest()
    {
    }


    public ReimbursementRequest(User employee, string courseName, decimal claimAmount)
    {
        Employee = employee;
        CourseName = courseName;
        ClaimAmount = claimAmount;
        RequestStatus = EnumRequestStatus.UNDER_REVIEW;
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