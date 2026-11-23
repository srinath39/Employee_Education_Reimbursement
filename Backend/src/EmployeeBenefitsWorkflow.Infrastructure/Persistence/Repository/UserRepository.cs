using EmployeeBenefitsWorkflow.Application.interfaces.Repository;
using EmployeeBenefitsWorkflow.Domain.Entities;

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

    public int UpdateUserByUserId(Guid userId,string updatedName)
    {
        User user= _db.Users.FirstOrDefault(user=>user.Id.Equals(userId))!;
        return _db.Sp_UpdateUser(updatedName,user.Email);
    }
}