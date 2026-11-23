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
    public async Task<IActionResult> GetSummary([FromHeader(Name = "UserId")] Guid hrId)
    {
        return Ok(await _service.GetHrStatistics(hrId));
    }

    [HttpGet("pendingRequests")]
    public async Task<IActionResult> GetAllPendingRequests()
    {
        return Ok(await _service.GetAllPendingRequestOfHr());
    }

    [HttpPost("request/{requestId:guid}/approve")]
    public async Task<IActionResult> ApproveRequest([FromHeader(Name = "UserId")] Guid hrId, Guid requestId)
    {
        await _service.ApproveRequest(hrId, requestId);
        return Ok();
    }

    [HttpPost("request/{requestId:guid}/reject")]
    public async Task<IActionResult> RejectRequest([FromHeader(Name = "UserId")] Guid hrId, Guid requestId)
    {
        await _service.RejectRequest(hrId, requestId);
        return Ok();
    }

}