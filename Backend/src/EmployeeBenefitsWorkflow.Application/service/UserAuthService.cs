namespace EmployeeBenefitsWorkflow.Application.service;

using EmployeeBenefitsWorkflow.Application.DTOs;
using EmployeeBenefitsWorkflow.Application.interfaces.Repository;
using EmployeeBenefitsWorkflow.Application.interfaces.Service;

public class UserAuthService : IAuthService
{
    private readonly IUserRepository _userRepository;

    public UserAuthService(IUserRepository userRepository)
    {
        _userRepository=userRepository;
    }
    
    public async Task<ResponseUserDto> UserLogin(RequestUserDto userDto)
    {
        // login logic 
        return new ResponseUserDto();
    }

    public async Task<ResponseUserDto> RegisterUser(RequestUserDto userDto)
    {
        // login logic 
        return new ResponseUserDto();
    }

    // testing puporse only 
    public async Task updateUserName(Guid userId,string updatedName)
    {
       await  _userRepository.UpdateUserByUserId(userId,updatedName);
    }


}