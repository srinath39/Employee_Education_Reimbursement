using EmployeeBenefitsWorkflow.Application.interfaces.Repository;
using EmployeeBenefitsWorkflow.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace EmployeeBenefitsWorkflow.Infrastructure.Persistence.Repository;

public class UserRepository : IUserRepository
{
    private readonly ApplicationDBContext _db;

    public UserRepository(ApplicationDBContext dbContext)
    {
        _db = dbContext;
    }

    public async Task<User> GetUserByUserId(Guid userId)
    {
        User? user=await _db.Users.Include(u => u.EmployeeRequests).Include(u => u.HrDecisions).FirstOrDefaultAsync(user => user.Id.Equals(userId))!;
        return user!;
    }

    public async Task<int> UpdateUserByUserId(Guid userId, string updatedName)
    {
        User? user = await _db.Users.FirstOrDefaultAsync(user => user.Id.Equals(userId))!;
        return _db.Sp_UpdateUser(updatedName, user!.Email);
    }
}