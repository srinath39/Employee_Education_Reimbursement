using EmployeeBenefitsWorkflow.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace EmployeeBenefitsWorkflow.Infrastructure.Persistence;

public class ApplicationDBContext : DbContext  // maps to SQL Database 
{

    public ApplicationDBContext(DbContextOptions options) : base(options)
    {
    }
    public DbSet<User> Users { get; set; }    // maps to SQL Database table 

    public DbSet<ReimbursementRequest> Requests {get;set;} 

    public DbSet<RequestDecisions> RequestDecisions {get;set;}



    // bind DBset to Database table
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<User>().ToTable("Users");
        modelBuilder.Entity<ReimbursementRequest>().ToTable("ReimbursementRequests");
        modelBuilder.Entity<RequestDecisions>().ToTable("RequestDecisions");

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


        string userJson = File.ReadAllText(filePath);   // imported from System.IO included by default 
        List<User> allUsersSeedDataList = System.Text.Json.JsonSerializer.Deserialize<List<User>>(userJson)!;
        foreach (User user in allUsersSeedDataList)
        {
            modelBuilder.Entity<User>().HasData(user);
        }
    }
}