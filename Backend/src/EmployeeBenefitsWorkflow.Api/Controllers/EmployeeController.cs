using EmployeeBenefitsWorkflow.Application.DTOs;
using EmployeeBenefitsWorkflow.Application.interfaces.Service;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing.Template;

namespace EmployeeBenefitsWorkflow.Api.Controllers;

[ApiController]
[Route("api/eduReimburse")]
public class EmployeeController : Controller
{
    private readonly IReimbursmentRequestService _service;
    private readonly IWebHostEnvironment _webHostEnvironmente;

    private string? rootPath;
    private string? webRootPath;
    private bool isDevelopment;

    public EmployeeController(IReimbursmentRequestService service, IWebHostEnvironment webHostEnvironmente)
    {
        _service = service;
        _webHostEnvironmente = webHostEnvironmente;
        rootPath = _webHostEnvironmente.ContentRootPath;
        webRootPath = _webHostEnvironmente.WebRootPath;
        isDevelopment = _webHostEnvironmente.IsDevelopment();
    }

    [HttpPost("")]
    public async Task<IActionResult> CreateRequest([FromHeader(Name ="UserId")] Guid empId,RequestReimbursmentDto request)
    {
        return Ok(await _service.CreateRequest(empId,request));

    }

    [HttpGet("mine")]
    public async Task<IActionResult> GetAllRequest([FromHeader(Name ="UserId")] Guid empId)
    {
        return Ok(await _service.GetAllRequestOfEmployee(empId));
    }

    [HttpGet("summary")]
    public async Task<IActionResult> GetSummary([FromHeader(Name ="UserId")] Guid empId)
    {
        return Ok(await _service.GetEmployeeStatistics(empId));
    }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetSpecificRequest(Guid id)
    {
        return Ok(await _service.GetRequestById(id));
    }

    // testing purpose 
    [HttpGet("env")]
    public async Task<IActionResult> GetEnvironmentDetails()
    {
        return Ok(rootPath+"\n"+webRootPath+"\n"+_webHostEnvironmente.EnvironmentName);
    }

}