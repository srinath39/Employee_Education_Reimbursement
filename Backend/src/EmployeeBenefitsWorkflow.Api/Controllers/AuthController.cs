using EmployeeBenefitsWorkflow.Application.DTOs;
using EmployeeBenefitsWorkflow.Application.interfaces.Service;
using EmployeeBenefitsWorkflow.Application.service;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeBenefitsWorkflow.Api.Controllers;

[ApiController]
[Route("api/auth")]
public class AuthController : Controller
{
    private readonly IAuthService _userAuthService;

    public AuthController(IAuthService userAuthService)
    {
        _userAuthService=userAuthService;
    }

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

    [HttpPost("{id:guid}/updateName")]
    public IActionResult UpdateUserName(Guid id,[FromQuery] string name)
    {
        _userAuthService.updateUserName(id,name);
        return Ok();
    }

}