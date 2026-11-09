namespace EmployeeBenefitsWorkflow.Application.DTOs;

public class ResponseReimbursmentDto
{
    public int RequestId {get;set;}

    public string? CourseName {get;set;}

    public double ClaimAmount {get;set;} 

    public string RequestStatus {get;set;} =string.Empty;

    public int EmpId {get;set;} 

    public string? EmployeeName {get;set;}
}