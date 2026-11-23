using EmployeeBenefitsWorkflow.Application.DTOs;

namespace EmployeeBenefitsWorkflow.Application.interfaces.Service;

public interface IAuthService
{
    public Task<ResponseUserDto> UserLogin(RequestUserDto userDto);

    public Task<ResponseUserDto> RegisterUser(RequestUserDto userDto);

    public Task updateUserName(Guid userId,string updatedName);
}