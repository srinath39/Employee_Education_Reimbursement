using EmployeeBenefitsWorkflow.Application.Exceptions;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeBenefitsWorkflow.Api.Controllers;


[ApiController]
[Route("api/error")]
public class ErrorController : Controller
{
    [Route("notify")]
    [ApiExplorerSettings(IgnoreApi = true)]
    public IActionResult GetErrorResponse()
    {
        IExceptionHandlerPathFeature? exceptionHandler = HttpContext.Features.Get<IExceptionHandlerPathFeature>();
        if (exceptionHandler is null)
            return NotFound();

        ServiceException? serviceException = exceptionHandler.Error as ServiceException;
        int status = serviceException?.StatusCode ?? StatusCodes.Status500InternalServerError;
        string message = serviceException?.Message ?? "An unexpected error occurred.";

        return Problem(
            statusCode: status,
            detail: message,
            instance: exceptionHandler.Path);
    }
}