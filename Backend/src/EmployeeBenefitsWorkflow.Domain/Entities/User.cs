
using EmployeeBenefitsWorkflow.Domain.Enums;

namespace EmployeeBenefitsWorkflow.Domain.Entities;

public class User
{
    public int Id { get; private set; }
    public string? Name { get; private set; }

    public EnumUserRoleType Role { get; private set; }

    public string? Email { get; private set; }

    public User(int id, string name, EnumUserRoleType role, string email)
    {
        if (id <= 0)
        {
            throw new InvalidDataException("Id is not valid");
        }

        if (string.IsNullOrEmpty(name))
        {
            throw new InvalidDataException("Name is null or empty");
        }

        if ((int)role > 1)
        {
            throw new InvalidDataException("Only Hr and Employee roles are allowed");
        }

        if (string.IsNullOrEmpty(email))
        {
            throw new InvalidDataException("Email is null or empty");
        }

        Id = Id;
        Name = name;
        Email = email;
        Role = role;
    }

}