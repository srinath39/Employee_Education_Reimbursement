
using System.ComponentModel.DataAnnotations;
using EmployeeBenefitsWorkflow.Domain.Enums;

namespace EmployeeBenefitsWorkflow.Domain.Entities;

public class User
{
    [Key]    // primary key in table 
    public Guid Id { get; private set; }

    [StringLength(100)]    // varchar(100) in table 
    [Required]
    public string Name { get; set; } =null!;

    [Required]
    public EnumUserRoleType Role { get; set; }

    [StringLength(100)]
    [Required]
    public string Email { get; set; } = null!;

    public User()
    {
        
    }

    public User(string name, EnumUserRoleType role, string email)
    {
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

        Id = Guid.NewGuid();
        Name = name;
        Email = email;
        Role = role;
    }

}