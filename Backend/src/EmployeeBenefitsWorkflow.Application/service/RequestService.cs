namespace EmployeeBenefitsWorkflow.Application.service;

using EmployeeBenefitsWorkflow.Application.DTOs;
using EmployeeBenefitsWorkflow.Application.interfaces.Service;
using EmployeeBenefitsWorkflow.Domain.Entities;
using EmployeeBenefitsWorkflow.Domain.Enums;

public class RequestService : IReimbursmentRequestService
{
    private readonly List<ReimbursementRequest> _reimbursementRequests;
    private readonly List<RequestDecisions> _requestDecisions;
    private int id;
    private int decisionId;

    public RequestService()
    {
        _reimbursementRequests = new List<ReimbursementRequest>();
        _requestDecisions = new List<RequestDecisions>();
        loadRequests();
        loadRequestsDecisions();
    }

    public void loadRequests()
    {
        _reimbursementRequests.Add(new ReimbursementRequest(0, new User(0,"srinath",EnumUserRoleType.EMPLOYEE,"srinath@mail.com"), "Azure", 3000.0, EnumRequestStatus.UNDER_REVIEW, null));
        _reimbursementRequests.Add(new ReimbursementRequest(0, new User(0,"srinath",EnumUserRoleType.EMPLOYEE,"srinath@mail.com"), "AWS", 4000.0, EnumRequestStatus.UNDER_REVIEW, null));
        _reimbursementRequests.Add(new ReimbursementRequest(0, new User(0,"srinath",EnumUserRoleType.EMPLOYEE,"srinath@mail.com"), "AI-901", 2000.0, EnumRequestStatus.APPROVED, null));
        _reimbursementRequests.Add(new ReimbursementRequest(0, new User(0,"srinath",EnumUserRoleType.EMPLOYEE,"srinath@mail.com"), "MERN", 3400.0, EnumRequestStatus.REJECTED, null));
        _reimbursementRequests.Add(new ReimbursementRequest(0, new User(0,"srinath",EnumUserRoleType.EMPLOYEE,"srinath@mail.com"), "Java", 2500.0, EnumRequestStatus.REJECTED, null));
    }

    public void loadRequestsDecisions()
    {
        _requestDecisions.Add(new RequestDecisions(1,_reimbursementRequests[0],new User(0,"srinath",EnumUserRoleType.EMPLOYEE,"srinath@mail.com"), EnumRequestStatus.UNDER_REVIEW));
        _requestDecisions.Add(new RequestDecisions(2,_reimbursementRequests[1], new User(0,"srinath",EnumUserRoleType.EMPLOYEE,"srinath@mail.com"), EnumRequestStatus.APPROVED));
        _requestDecisions.Add(new RequestDecisions(3,_reimbursementRequests[2], new User(0,"srinath",EnumUserRoleType.EMPLOYEE,"srinath@mail.com"), EnumRequestStatus.REJECTED));
        _requestDecisions.Add(new RequestDecisions(4,_reimbursementRequests[3],new User(0,"srinath",EnumUserRoleType.EMPLOYEE,"srinath@mail.com"), EnumRequestStatus.REJECTED));
    }

    public ResponseEmployeeStatistics GetEmployeeStatistics(int employeeId)
    {
        // total submitted requests
        // total Under review requests
        // total rejected requests
        // total approved requests
        List<ReimbursementRequest> totalEmpRequest = _reimbursementRequests.Where(request => request.Employee != null && request.Employee.Id == employeeId).ToList();
        int totalRequests = totalEmpRequest.Count;
        int totalUnderReviewRequests = totalEmpRequest.Count(request => request.RequestStatus == EnumRequestStatus.UNDER_REVIEW);
        int totalRejectedRequests = totalEmpRequest.Count(request => request.RequestStatus == EnumRequestStatus.REJECTED);
        int totalApprovedRequests = totalEmpRequest.Count(request => request.RequestStatus == EnumRequestStatus.APPROVED);
        return new ResponseEmployeeStatistics()
        {
            TotalEmployeeRequests = totalRequests,
            TotalUnderReviewRequests = totalUnderReviewRequests,
            TotalRejectedRequests = totalRejectedRequests,
            TotalApprovedRequests = totalApprovedRequests
        };
    }

    public ResponseReimbursmentDto CreateRequest(RequestReimbursmentDto requestDto)
    {
        // user as employee id is required here 
        ReimbursementRequest request = new ReimbursementRequest(id++, requestDto.CourseName, requestDto.ClaimAmount);
        _reimbursementRequests.Add(request);
        return GetResponseRequestDtoFromRequestEntity(request);
    }

    public List<ResponseReimbursmentDto> GetAllRequestOfEmployee(int employeeId)
    {
        return _reimbursementRequests.Where(request => request.Employee != null && request.Employee.Id == employeeId).Select(request => GetResponseRequestDtoFromRequestEntity(request)).ToList();
    }

    public ResponseReimbursmentDto GetRequestById(int RequestId)  // shared accross both HR and Employee 
    {
        ReimbursementRequest? req = _reimbursementRequests.FirstOrDefault(request => request.RequestId == RequestId);
        if (req is null)
        {
            throw new KeyNotFoundException("Request not found");
        }
        return GetResponseRequestDtoFromRequestEntity(req);
    }


    public ResponseHrStatistics GetHrStatistics(int hrId)
    {
        // total decisions done -> from request Decisions belongs to hrId
        List<RequestDecisions> empDecisions = _requestDecisions.Where(request => request.Hr.Id == hrId).ToList();
        int totalDecisionsGiven = empDecisions.Count;
        // total approved decisions
        int totalApprovedDecisions = empDecisions.Count(request => request.RequestStatus == EnumRequestStatus.APPROVED);
        // total Rejected decisions
        int totalRejectedDecisions = empDecisions.Count(request => request.RequestStatus == EnumRequestStatus.REJECTED);
        // total pending request  -> among all request which request as underreview status
        int totalPendingRequests = _reimbursementRequests.Count(request => request.RequestStatus == EnumRequestStatus.UNDER_REVIEW);
        return new ResponseHrStatistics()
        {
            TotalDecisionsGiven = totalDecisionsGiven,
            TotalApprovedRequests = totalApprovedDecisions,
            TotalRejectedRequests = totalRejectedDecisions,
            TotalPendingRequests = totalPendingRequests
        };
    }

    public List<ResponseReimbursmentDto> GetAllPendingRequestOfHr()  // same for all HR's 
    {
        return _reimbursementRequests.Where(request => request.RequestStatus == EnumRequestStatus.UNDER_REVIEW).ToList().Select(request => GetResponseRequestDtoFromRequestEntity(request)).ToList();
    }

    public void ApproveRequest(int requestId)
    {
        // HR id is required here 
        // steps :
        // current request with ID need to change status as APPROVE
        // A new RequestDecisions need to be created with request and HR details along with Decision
    }

    public void RejectRequest(int requestId)
    {
        // HR id is required here 
        // steps :
        // current request with ID need to change status as REJECTED
        // A new RequestDecisions need to be created with request and HR details along with Decision
    }

    private ResponseReimbursmentDto GetResponseRequestDtoFromRequestEntity(ReimbursementRequest entityRequest)
    {
        return new ResponseReimbursmentDto()
        {
            RequestId = entityRequest.RequestId,
            ClaimAmount = entityRequest.ClaimAmount,
            CourseName = entityRequest.CourseName,
            RequestStatus = entityRequest.RequestStatus.ToString()
        };
    }
}