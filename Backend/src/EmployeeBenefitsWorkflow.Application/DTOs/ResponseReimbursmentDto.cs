namespace EmployeeBenefitsWorkflow.Application.DTOs;

public class ResponseReimbursmentDto
{
    public Guid RequestId {get;set;}

    public string? CourseName {get;set;}

    public decimal ClaimAmount {get;set;} 

    public string RequestStatus {get;set;} =string.Empty;

    public Guid EmpId {get;set;} 

    public string? EmployeeName {get;set;}
}