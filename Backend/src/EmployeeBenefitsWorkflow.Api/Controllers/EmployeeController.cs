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
    public IActionResult CreateRequest(RequestReimbursmentDto request)
    {
        return Ok(_service.CreateRequest(request));

    }

    [HttpGet("mine")]
    public IActionResult GetAllRequest()
    {
        return Ok(_service.GetAllRequestOfEmployee(Guid.NewGuid()));
    }

    [HttpGet("summary")]
    public IActionResult GetSummary()
    {
        // int id=employee id from cookies/tokens 
        return Ok(_service.GetEmployeeStatistics(Guid.NewGuid()));
    }

    [HttpGet("{id:int}")]
    public IActionResult GetSpecificRequest(int id)
    {
        return Ok(_service.GetRequestById(id));
    }

    [HttpGet("env")]
    public IActionResult GetEnvironmentDetails()
    {
        return Ok(rootPath+"\n"+webRootPath+"\n"+_webHostEnvironmente.EnvironmentName);
    }

}