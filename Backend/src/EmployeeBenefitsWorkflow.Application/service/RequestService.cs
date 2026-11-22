namespace EmployeeBenefitsWorkflow.Application.service;

using EmployeeBenefitsWorkflow.Application.DTOs;
using EmployeeBenefitsWorkflow.Application.interfaces.Repository;
using EmployeeBenefitsWorkflow.Application.interfaces.Service;
using EmployeeBenefitsWorkflow.Domain.Entities;
using EmployeeBenefitsWorkflow.Domain.Enums;

public class RequestService : IReimbursmentRequestService
{
    private readonly IRequestRepository _requestRepository;

    private readonly IRequestDecisionRepository _requestDecisionRepository;
    private readonly IUserRepository _userRepository;

    public RequestService(IRequestRepository requestRepository, IRequestDecisionRepository requestDecisionRepository, IUserRepository userRepository)
    {
        _requestRepository = requestRepository;
        _requestDecisionRepository = requestDecisionRepository;
        _userRepository = userRepository;
    }

    public ResponseEmployeeStatistics GetEmployeeStatistics(Guid employeeId)
    {
        // total submitted requests
        // total Under review requests
        // total rejected requests
        // total approved requests
        List<ReimbursementRequest> totalEmpRequest = _requestRepository.GetAllRequestByEmployeeId(employeeId).Where(request => request != null).ToList();
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

    public ResponseReimbursmentDto CreateRequest(Guid empId, RequestReimbursmentDto requestDto)
    {
        // user as employee id is required here 
        User emp = _userRepository.GetUserByUserId(empId);
        ReimbursementRequest request = new ReimbursementRequest(emp, requestDto.CourseName, requestDto.ClaimAmount);
        Guid requestId = _requestRepository.CreateRequest(request);
        return GetResponseRequestDtoFromRequestEntity(_requestRepository.GetRequestByRequestId(requestId));
    }

    public List<ResponseReimbursmentDto> GetAllRequestOfEmployee(Guid employeeId)
    {
        return _requestRepository.GetAllRequestByEmployeeId(employeeId).Where(request => request != null).Select(request => GetResponseRequestDtoFromRequestEntity(request)).ToList();
    }

    public ResponseReimbursmentDto GetRequestById(Guid requestId)  // shared accross both HR and Employee 
    {
        ReimbursementRequest? req = _requestRepository.GetRequestByRequestId(requestId);
        if (req is null)
        {
            throw new KeyNotFoundException("Request not found");
        }
        return GetResponseRequestDtoFromRequestEntity(req);
    }


    public ResponseHrStatistics GetHrStatistics(Guid hrId)
    {
        // total decisions done -> from request Decisions belongs to hrId
        List<RequestDecisions> empDecisions = _requestDecisionRepository.GetRequestDecisionByHrId(hrId).Where(requestDecision => requestDecision != null).ToList();
        int totalDecisionsGiven = empDecisions.Count;
        // total approved decisions
        int totalApprovedDecisions = empDecisions.Count(request => request.RequestStatus == EnumRequestStatus.APPROVED);
        // total Rejected decisions
        int totalRejectedDecisions = empDecisions.Count(request => request.RequestStatus == EnumRequestStatus.REJECTED);
        // total pending request  -> among all request which request as underreview status
        int totalPendingRequests = _requestRepository.GetAllRequestByRequestStatus(EnumRequestStatus.UNDER_REVIEW).Where(request => request != null).ToList().Count;
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
        return _requestRepository.GetAllRequestByRequestStatus(EnumRequestStatus.UNDER_REVIEW).Where(request => request != null).Select(request => GetResponseRequestDtoFromRequestEntity(request)).ToList();
    }

    public void ApproveRequest(Guid hrId, Guid requestId)
    {
        // HR id is required here 
        User hr = _userRepository.GetUserByUserId(hrId);
        // steps :
        // current request with ID need to change status as APPROVE
        ReimbursementRequest request = _requestRepository.GetRequestByRequestId(requestId);
        if (request is null)
        {
            throw new NullReferenceException("Request is null");
        }
        request.ApproveRequest();
        _requestRepository.SaveRequestChanges();
        // A new RequestDecisions need to be created with request and HR details along with Decision
        var requestDecision = new RequestDecisions(request, hr, request.RequestStatus);
        _requestDecisionRepository.CreateRequestDecision(requestDecision);
        _requestDecisionRepository.SaveRequestDecisionsChanges();
    }

    public void RejectRequest(Guid hrId, Guid requestId)
    {
        // HR id is required here 
        User hr = _userRepository.GetUserByUserId(hrId);
        // steps :
        // current request with ID need to change status as REJECTED
        ReimbursementRequest request = _requestRepository.GetRequestByRequestId(requestId);
        if (request is null)
        {
            throw new NullReferenceException("Request is null");
        }
        request.RejectRequest();
        _requestRepository.SaveRequestChanges();
        // A new RequestDecisions need to be created with request and HR details along with Decision
        var requestDecision = new RequestDecisions(request, hr, request.RequestStatus);
        _requestDecisionRepository.CreateRequestDecision(requestDecision);
        _requestDecisionRepository.SaveRequestDecisionsChanges();
    }

    private ResponseReimbursmentDto GetResponseRequestDtoFromRequestEntity(ReimbursementRequest entityRequest)
    {
        return new ResponseReimbursmentDto()
        {
            RequestId = entityRequest.RequestId,
            ClaimAmount = entityRequest.ClaimAmount,
            CourseName = entityRequest.CourseName,
            RequestStatus = entityRequest.RequestStatus.ToString(),
            EmpId = entityRequest.EmployeeId,
            EmployeeName = entityRequest.Employee?.Name
        };
    }
}
