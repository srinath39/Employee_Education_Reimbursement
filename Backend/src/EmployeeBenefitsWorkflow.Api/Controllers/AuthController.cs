using EmployeeBenefitsWorkflow.Application.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeBenefitsWorkflow.Api.Controllers;

[ApiController]
[Route("api/auth")]
public class AuthController : Controller
{
    [HttpPost("register")]
    public IActionResult UseRegister(RequestUserDto user)
    {
        return Ok(user);
    }

    [HttpPost("login")]
    public IActionResult UserLogin(RequestUserDto user)
    {
        return Ok(user);
    }

    [HttpGet("me")]
    public IActionResult GetUser()
    {
        return Ok();
    }

}