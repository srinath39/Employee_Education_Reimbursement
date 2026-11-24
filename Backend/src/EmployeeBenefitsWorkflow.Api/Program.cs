using EmployeeBenefitsWorkflow.Application.interfaces.Service;
using EmployeeBenefitsWorkflow.Application.service;
using Microsoft.EntityFrameworkCore;
using EmployeeBenefitsWorkflow.Infrastructure.Persistence;
using EmployeeBenefitsWorkflow.Application.interfaces.Repository;
using EmployeeBenefitsWorkflow.Infrastructure.Persistence.Repository;
using EmployeeBenefitsWorkflow.Api.Middleware;

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

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
if (string.IsNullOrWhiteSpace(connectionString))
{
    throw new InvalidOperationException(
        "ConnectionStrings:DefaultConnection is missing. Set it with .NET User Secrets in Development " +
        "or with the ConnectionStrings__DefaultConnection environment variable.");
}

builder.Services.AddDbContext<ApplicationDBContext>(options => options.UseSqlServer(connectionString));

// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

builder.Services.AddScoped<IReimbursmentRequestService, RequestService>();
builder.Services.AddScoped<IAuthService, UserAuthService>();


builder.Services.AddScoped<IRequestDecisionRepository, RequestDecisionRepository>();
builder.Services.AddScoped<IRequestRepository, RequestRepository>();
builder.Services.AddScoped<IUserRepository, UserRepository>();



var app = builder.Build();


app.UseExceptionHandler("/api/error/notify");
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())   // app.Environment is IWebHostEnvironment   
{
    app.UseGlobalExcpetionMiddleWare();  // testing purpose
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
