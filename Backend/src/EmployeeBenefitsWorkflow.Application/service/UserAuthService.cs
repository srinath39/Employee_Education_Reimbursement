namespace EmployeeBenefitsWorkflow.Application.service;

using EmployeeBenefitsWorkflow.Application.DTOs;
using EmployeeBenefitsWorkflow.Application.interfaces.Service;

public class UserAuthService : IAuthService
{
    public ResponseUserDto UserLogin(RequestUserDto userDto)
    {
        // login logic 
        return new ResponseUserDto();
    }

    public ResponseUserDto RegisterUser(RequestUserDto userDto)
    {
        // login logic 
        return new ResponseUserDto();
    }
}