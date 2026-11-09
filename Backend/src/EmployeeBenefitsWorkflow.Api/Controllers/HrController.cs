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
        _service=service;
    }

    [HttpGet("summary")]
    public IActionResult GetSummary()
    {
        return Ok(_service.GetHrStatistics(0));
    }

    [HttpGet("pendingRequests")]
    public IActionResult GetAllPendingRequests()
    {
        return Ok(_service.GetAllPendingRequestOfHr());
    }

    [HttpPost("request/{id:int}/approve")]
    public IActionResult ApproveRequest(int id)
    {
        _service.ApproveRequest(id);
        return Ok();
    }

    [HttpPost("request/{id:int}/reject")]
    public IActionResult RejectRequest(int id)
    {
        _service.RejectRequest(id);
        return Ok();
    }

}