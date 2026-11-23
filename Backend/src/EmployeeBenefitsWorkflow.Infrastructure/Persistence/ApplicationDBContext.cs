using EmployeeBenefitsWorkflow.Domain.Entities;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;

namespace EmployeeBenefitsWorkflow.Infrastructure.Persistence;

public class ApplicationDBContext : DbContext
{
    public ApplicationDBContext(
        DbContextOptions<ApplicationDBContext> options)
        : base(options)
    {
    }

    public DbSet<User> Users { get; set; }
    public DbSet<ReimbursementRequest> Requests { get; set; }
    public DbSet<RequestDecisions> RequestDecisions { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Fluent API 

        // User configuration
        modelBuilder.Entity<User>(entity =>
        {
            entity.ToTable("Users");

            entity.HasKey(u => u.Id);

            entity.Property(u => u.Id)
                .HasColumnType("uniqueidentifier");
        });

        // ReimbursementRequest configuration
        modelBuilder.Entity<ReimbursementRequest>().Property(request=>request.ClaimAmount)
        .HasColumnName("ClaimExpense")
        .HasColumnType("decimal(10,2)")
        .HasDefaultValue("0.0");

        modelBuilder.Entity<ReimbursementRequest>(entity =>
        {
            entity.ToTable("ReimbursementRequests");

            entity.HasKey(r => r.RequestId);

            entity.Property(r => r.RequestId)
                .HasColumnType("uniqueidentifier");

            entity.Property(r => r.EmployeeId)
                .HasColumnType("uniqueidentifier");

            entity.HasOne(r => r.Employee)
                .WithMany(u => u.EmployeeRequests)
                .HasForeignKey(r => r.EmployeeId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Restrict);
        });

        // RequestDecisions configuration
        modelBuilder.Entity<RequestDecisions>(entity =>
        {
            entity.ToTable("RequestDecisions");

            entity.Property(r => r.RequestId)
                .HasColumnType("uniqueidentifier");

            entity.Property(r => r.HrId)
                .HasColumnType("uniqueidentifier");

            entity.HasOne(r => r.Request)
                .WithOne()
                .HasForeignKey<RequestDecisions>(r => r.RequestId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Restrict);

            entity.HasOne(r => r.Hr)
                .WithMany()
                .HasForeignKey(r => r.HrId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Restrict);
        });

        // set schema
        modelBuilder.HasDefaultSchema("dbo");


        // Seed data
        var filePath = Path.GetFullPath(
            Path.Combine(
                Directory.GetCurrentDirectory(),
                "..",
                "EmployeeBenefitsWorkflow.Infrastructure",
                "Persistence",
                "SeedDataJson",
                "UserJson.json"
            )
        );

        string userJson = File.ReadAllText(filePath);

        var users = JsonSerializer.Deserialize<List<User>>(userJson)
            ?? throw new InvalidOperationException(
                "Unable to deserialize user seed data.");

        modelBuilder.Entity<User>().HasData(users);
    }

    // Stored Procedure 
    // use DBset for select operations, and Database Property for INSERT/UPDATE/DELETE operations 

    public List<ReimbursementRequest> Sp_getAllRequests()
    {
        return Requests.FromSqlRaw("EXECUTE [dbo].[GetAllRequest]").ToList();
    }

    public int Sp_InsertNewUser(User user)
    {
        SqlParameter[] parameters=new SqlParameter[]
        {
            new SqlParameter("@UserName",user.Name),
            new SqlParameter("@UserRole",user.Role),
            new SqlParameter("@UserEmail",user.Email)
        };
        return Database.ExecuteSqlRaw("EXECUTE [dbo].[GetAllRequest] @UserName @UserRole @UserEmail",parameters);
    }

    public int Sp_UpdateUser(string newName,string email)
    {
        SqlParameter[] parameters=new SqlParameter[]
        {
            new SqlParameter("@UserName",newName),
            new SqlParameter("@UserEmail",email),
        };
        return Database.ExecuteSqlRaw("EXECUTE [dbo].[updateUser] @UserName,@UserEmail",parameters);
    }
}