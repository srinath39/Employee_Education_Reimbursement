
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace EmployeeBenefitsWorkflow.Application.DTOs;


public class RequestReimbursmentDto
{
    [Required(ErrorMessage ="{0} is Required")]
    public string CourseName {get; set;} = string.Empty;

    [Required(ErrorMessage ="{0} is required")]
    [DisplayName("Claim Amount")]
    [Range(2000.0,5000.0,ErrorMessage ="{0} should be between {1} and {2}")]
    public double ClaimAmount {get; set;}
}