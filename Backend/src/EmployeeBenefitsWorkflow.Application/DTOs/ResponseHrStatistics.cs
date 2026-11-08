namespace EmployeeBenefitsWorkflow.Application.DTOs;

public class ResponseHrStatistics
{
    public int TotalDecisionsGiven {get;set;}

    public int TotalApprovedRequests {get;set;}

    public int TotalRejectedRequests {get;set;}

    public int TotalPendingRequests {get;set;}
}