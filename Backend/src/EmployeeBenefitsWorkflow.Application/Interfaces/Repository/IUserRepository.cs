using EmployeeBenefitsWorkflow.Domain.Entities;

namespace EmployeeBenefitsWorkflow.Application.interfaces.Repository;

public interface IUserRepository
{
    public User GetUserByUserId(Guid userId);
}