using EmployeeBenefitsWorkflow.Application.DTOs;

namespace EmployeeBenefitsWorkflow.Application.interfaces.Service;

public interface IAuthService
{
    public ResponseUserDto UserLogin(RequestUserDto userDto);

    public ResponseUserDto RegisterUser(RequestUserDto userDto);
}