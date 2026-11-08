namespace EmployeeBenefitsWorkflow.Application.DTOs;

public class ResponseEmployeeStatistics
{
    public int TotalEmployeeRequests {get;set;}

    public int TotalUnderReviewRequests {get;set;}

    public int TotalRejectedRequests {get;set;}

    public int TotalApprovedRequests {get;set;}
}