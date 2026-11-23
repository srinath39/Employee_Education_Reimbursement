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

    // testing puporse only 
    public void updateUserName(Guid userId,string updatedName)
    {
        _userRepository.UpdateUserByUserId(userId,updatedName);
    }


}