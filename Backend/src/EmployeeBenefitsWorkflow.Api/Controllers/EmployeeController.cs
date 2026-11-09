using EmployeeBenefitsWorkflow.Application.DTOs;
using EmployeeBenefitsWorkflow.Application.interfaces.Service;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeBenefitsWorkflow.Api.Controllers;

[ApiController]
[Route("api/eduReimburse")]
public class EmployeeController : Controller
{
    private readonly IReimbursmentRequestService _service;

    public EmployeeController(IReimbursmentRequestService service)
    {
        _service=service;
    }

    [HttpPost("")]
    public IActionResult CreateRequest(RequestReimbursmentDto request)
    {
        return Ok(_service.CreateRequest(request));

    }

    [HttpGet("mine")]
    public IActionResult GetAllRequest()
    {
        return Ok(_service.GetAllRequestOfEmployee(0));
    }

    [HttpGet("summary")]
    public IActionResult GetSummary()
    {
        // int id=employee id from cookies/tokens 
        return Ok(_service.GetEmployeeStatistics(0));
    }

    [HttpGet("{id:int}")]
    public IActionResult GetSpecificRequest(int id)
    {
        return Ok(_service.GetRequestById(id));
    }

}