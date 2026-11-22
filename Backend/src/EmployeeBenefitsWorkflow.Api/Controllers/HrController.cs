using EmployeeBenefitsWorkflow.Application.DTOs;
using EmployeeBenefitsWorkflow.Application.interfaces.Service;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeBenefitsWorkflow.Api.Controllers;

[ApiController]
[Route("api/hr")]
public class HrController : Controller
{
    private readonly IReimbursmentRequestService _service;

    public HrController(IReimbursmentRequestService service)
    {
        _service = service;
    }

    [HttpGet("summary")]
    public IActionResult GetSummary([FromHeader(Name = "UserId")] Guid hrId)
    {
        return Ok(_service.GetHrStatistics(hrId));
    }

    [HttpGet("pendingRequests")]
    public IActionResult GetAllPendingRequests()
    {
        return Ok(_service.GetAllPendingRequestOfHr());
    }

    [HttpPost("request/{requestId:guid}/approve")]
    public IActionResult ApproveRequest([FromHeader(Name = "UserId")] Guid hrId, Guid requestId)
    {
        _service.ApproveRequest(hrId, requestId);
        return Ok();
    }

    [HttpPost("request/{requestId:guid}/reject")]
    public IActionResult RejectRequest([FromHeader(Name = "UserId")] Guid hrId, Guid requestId)
    {
        _service.RejectRequest(hrId, requestId);
        return Ok();
    }

}