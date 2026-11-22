using EmployeeBenefitsWorkflow.Application.interfaces.Service;
using EmployeeBenefitsWorkflow.Application.service;
using Microsoft.EntityFrameworkCore;
using EmployeeBenefitsWorkflow.Infrastructure.Persistence;
using EmployeeBenefitsWorkflow.Application.interfaces.Repository;
using EmployeeBenefitsWorkflow.Infrastructure.Persistence.Repository;

var builder = WebApplication.CreateBuilder(args);


// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddCors(options =>
{
    options.AddPolicy("Frontend", policy =>
    {
        // only for development purpose now 
        policy
            .WithOrigins("http://localhost:5173")
            .AllowAnyHeader()
            .AllowAnyMethod();
    });
});

builder.Services.AddDbContext<ApplicationDBContext>(options =>   // dependency injection    // bu default it is scoped service 
{
    options.UseMySQL(builder.Configuration.GetConnectionString("DefaultConnection")!);    // telling the EF core that we are using SQL Server / MY SQl , and telling it to connect to the database using connection string 
}); 

// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

builder.Services.AddScoped<IReimbursmentRequestService, RequestService>();
builder.Services.AddScoped<IAuthService, UserAuthService>();


builder.Services.AddScoped<IRequestDecisionRepository, RequestDecisionRepository>();
builder.Services.AddScoped<IRequestRepository, RequestRepository>();
builder.Services.AddScoped<IUserRepository, UserRepository>();



var app = builder.Build();


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())   // app.Environment is IWebHostEnvironment   
{
    app.UseDeveloperExceptionPage();
}
// app.UseHttpsRedirection();

app.UseCors("Frontend");

app.UseAuthorization();

app.MapControllers();


if (app.Environment.IsDevelopment())
{
    app.MapFallback(async (context) =>
    {
        await context.Response.WriteAsync("All fallback end point middleware");
    });
}

app.Run();
