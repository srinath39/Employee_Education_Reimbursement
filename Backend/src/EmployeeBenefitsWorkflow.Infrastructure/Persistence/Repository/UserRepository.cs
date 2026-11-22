using EmployeeBenefitsWorkflow.Application.interfaces.Repository;
using EmployeeBenefitsWorkflow.Domain.Entities;
using ZstdSharp.Unsafe;

namespace EmployeeBenefitsWorkflow.Infrastructure.Persistence.Repository;

public class UserRepository : IUserRepository
{
    private readonly ApplicationDBContext _db;

    public UserRepository(ApplicationDBContext dbContext)
    {
        _db=dbContext;
    }

    public User GetUserByUserId(Guid userId)
    {
        return _db.Users.FirstOrDefault(user=>user.Id.Equals(userId))!;
    }
}