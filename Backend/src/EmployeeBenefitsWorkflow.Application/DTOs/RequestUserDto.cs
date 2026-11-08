using System.ComponentModel.DataAnnotations;

namespace EmployeeBenefitsWorkflow.Application.DTOs;

public class RequestUserDto
{
    [Required]
    public string? Name {get;set;}

    [Required]
    public string? Role {get;set;}

    [Required]
    [EmailAddress(ErrorMessage ="{0} is in wrong format")]
    public string? Email {get;set;}
}