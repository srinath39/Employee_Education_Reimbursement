namespace EmployeeBenefitsWorkflow.Application.service;

using EmployeeBenefitsWorkflow.Application.DTOs;
using EmployeeBenefitsWorkflow.Application.Exceptions;
using EmployeeBenefitsWorkflow.Application.interfaces.Repository;
using EmployeeBenefitsWorkflow.Application.interfaces.Service;

public class UserAuthService : IAuthService
{
    private readonly IUserRepository _userRepository;

    public UserAuthService(IUserRepository userRepository)
    {
        _userRepository=userRepository;
    }
    
    public Task<ResponseUserDto> UserLogin(RequestUserDto userDto)
    {
        throw new FeatureNotImplementedException("Login is not implemented.");
    }

    public Task<ResponseUserDto> RegisterUser(RequestUserDto userDto)
    {
        throw new FeatureNotImplementedException("User registration is not implemented.");
    }

    // testing puporse only 
    public async Task updateUserName(Guid userId,string updatedName)
    {
        if (userId == Guid.Empty)
            throw new BadRequestException("User ID is required.");
        if (string.IsNullOrWhiteSpace(updatedName) || updatedName.Length > 100)
            throw new BadRequestException("User name must contain between 1 and 100 characters.");
        if (await _userRepository.GetUserByUserId(userId) is null)
            throw new NotFoundException("User not found.");

        await _userRepository.UpdateUserByUserId(userId, updatedName);
    }


}