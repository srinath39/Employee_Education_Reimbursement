using EmployeeBenefitsWorkflow.Domain.Entities;

namespace EmployeeBenefitsWorkflow.Application.interfaces.Repository;

public interface IUserRepository
{
    public Task<User> GetUserByUserId(Guid userId);

    public Task<int> UpdateUserByUserId(Guid userId,string updatedName);
}