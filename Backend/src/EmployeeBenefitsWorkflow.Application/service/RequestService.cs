namespace EmployeeBenefitsWorkflow.Application.service;

using EmployeeBenefitsWorkflow.Application.DTOs;
using EmployeeBenefitsWorkflow.Application.Exceptions;
using EmployeeBenefitsWorkflow.Application.interfaces.Repository;
using EmployeeBenefitsWorkflow.Application.interfaces.Service;
using EmployeeBenefitsWorkflow.Domain.Entities;
using EmployeeBenefitsWorkflow.Domain.Enums;
using System.ComponentModel.DataAnnotations;

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

    public async Task<ResponseEmployeeStatistics> GetEmployeeStatistics(Guid employeeId)
    {
        ValidateId(employeeId, "Employee ID");
        // total submitted requests
        // total Under review requests
        // total rejected requests
        // total approved requests
        List<ReimbursementRequest> totalEmpRequest = await _requestRepository.GetAllRequestByEmployeeId(employeeId);
        totalEmpRequest=totalEmpRequest.Where(request => request != null).ToList();
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

    public async Task<ResponseReimbursmentDto> CreateRequest(Guid empId, RequestReimbursmentDto requestDto)
    {
        ValidateId(empId, "Employee ID");
        if (requestDto is null)
            throw new BadRequestException("Request details are required.");

        var validationResults = new List<ValidationResult>();
        if (!Validator.TryValidateObject(requestDto, new ValidationContext(requestDto), validationResults, true))
            throw new BadRequestException(validationResults[0].ErrorMessage ?? "Invalid request details.");
        if (requestDto.CourseName.Length > 100)
            throw new BadRequestException("Course name must not exceed 100 characters.");

        User emp = await _userRepository.GetUserByUserId(empId)
            ?? throw new NotFoundException("Employee not found.");
        ReimbursementRequest request = new ReimbursementRequest(emp, requestDto.CourseName, requestDto.ClaimAmount);
        Guid requestId = await _requestRepository.CreateRequest(request);
        ReimbursementRequest savedRequest = await _requestRepository.GetRequestByRequestId(requestId)
            ?? throw new InternalServerException("The saved reimbursement request could not be retrieved.");
        return GetResponseRequestDtoFromRequestEntity(savedRequest);
    }

    public async Task<List<ResponseReimbursmentDto>> GetAllRequestOfEmployee(Guid employeeId)
    {
        ValidateId(employeeId, "Employee ID");
        List<ReimbursementRequest> totalEmpRequest = await _requestRepository.GetAllRequestByEmployeeId(employeeId);
        return totalEmpRequest.Where(request => request != null).Select(request => GetResponseRequestDtoFromRequestEntity(request)).ToList();
    }

    public async Task<ResponseReimbursmentDto> GetRequestById(Guid requestId)  // shared accross both HR and Employee 
    {
        ValidateId(requestId, "Request ID");
        ReimbursementRequest? req = await _requestRepository.GetRequestByRequestId(requestId);
        if (req is null)
        {
            throw new NotFoundException("Request not found.");
        }
        return GetResponseRequestDtoFromRequestEntity(req);
    }


    public async Task<ResponseHrStatistics> GetHrStatistics(Guid hrId)
    {
        ValidateId(hrId, "HR ID");
        // total decisions done -> from request Decisions belongs to hrId
        List<RequestDecisions> empDecisions = await _requestDecisionRepository.GetRequestDecisionByHrId(hrId);
        empDecisions=empDecisions.Where(requestDecision => requestDecision != null).ToList();
        int totalDecisionsGiven = empDecisions.Count;
        // total approved decisions
        int totalApprovedDecisions = empDecisions.Count(request => request.RequestStatus == EnumRequestStatus.APPROVED);
        // total Rejected decisions
        int totalRejectedDecisions = empDecisions.Count(request => request.RequestStatus == EnumRequestStatus.REJECTED);
        // total pending request  -> among all request which request as underreview status
        List<ReimbursementRequest> totalEmpRequests=await _requestRepository.GetAllRequestByRequestStatus(EnumRequestStatus.UNDER_REVIEW);
        int totalPendingRequests = totalEmpRequests.Where(request => request != null).ToList().Count;
        return new ResponseHrStatistics()
        {
            TotalDecisionsGiven = totalDecisionsGiven,
            TotalApprovedRequests = totalApprovedDecisions,
            TotalRejectedRequests = totalRejectedDecisions,
            TotalPendingRequests = totalPendingRequests
        };
    }

    public async Task<List<ResponseReimbursmentDto>> GetAllPendingRequestOfHr()  // same for all HR's 
    {
        List<ReimbursementRequest> underReviewRequests=await _requestRepository.GetAllRequestByRequestStatus(EnumRequestStatus.UNDER_REVIEW);
        return underReviewRequests.Where(request => request != null).Select(request => GetResponseRequestDtoFromRequestEntity(request)).ToList();
    }

    public async Task ApproveRequest(Guid hrId, Guid requestId)
    {
        ValidateId(hrId, "HR ID");
        ValidateId(requestId, "Request ID");
        User hr = await _userRepository.GetUserByUserId(hrId)
            ?? throw new NotFoundException("HR user not found.");
        // steps :
        // current request with ID need to change status as APPROVE
        ReimbursementRequest request = await _requestRepository.GetRequestByRequestId(requestId)
            ?? throw new NotFoundException("Request not found.");
        EnsureUnderReview(request);
        request.ApproveRequest();
        await _requestRepository.SaveRequestChanges();
        // A new RequestDecisions need to be created with request and HR details along with Decision
        var requestDecision = new RequestDecisions(request, hr, request.RequestStatus);
        await _requestDecisionRepository.CreateRequestDecision(requestDecision);
        await _requestDecisionRepository.SaveRequestDecisionsChanges();
    }

    public async Task RejectRequest(Guid hrId, Guid requestId)
    {
        ValidateId(hrId, "HR ID");
        ValidateId(requestId, "Request ID");
        User hr = await _userRepository.GetUserByUserId(hrId)
            ?? throw new NotFoundException("HR user not found.");
        // steps :
        // current request with ID need to change status as REJECTED
        ReimbursementRequest request = await _requestRepository.GetRequestByRequestId(requestId)
            ?? throw new NotFoundException("Request not found.");
        EnsureUnderReview(request);
        request.RejectRequest();
        await _requestRepository.SaveRequestChanges();
        // A new RequestDecisions need to be created with request and HR details along with Decision
        var requestDecision = new RequestDecisions(request, hr, request.RequestStatus);
        await _requestDecisionRepository.CreateRequestDecision(requestDecision);
        await _requestDecisionRepository.SaveRequestDecisionsChanges();
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

    private static void ValidateId(Guid id, string name)
    {
        if (id == Guid.Empty)
            throw new BadRequestException($"{name} is required.");
    }

    private static void EnsureUnderReview(ReimbursementRequest request)
    {
        if (request.RequestStatus != EnumRequestStatus.UNDER_REVIEW)
            throw new ConflictException("Only requests under review can be approved or rejected.");
    }
}
