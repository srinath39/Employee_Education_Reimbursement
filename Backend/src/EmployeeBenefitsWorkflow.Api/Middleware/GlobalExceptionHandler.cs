using System.ComponentModel.Design.Serialization;

namespace EmployeeBenefitsWorkflow.Api.Middleware;

public class GlobalExceptionHandler
{
    private readonly RequestDelegate _next;
    private ILogger<GlobalExceptionHandler> _logger;

    public GlobalExceptionHandler(RequestDelegate next, ILogger<GlobalExceptionHandler> logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task Invoke(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception ex)
        {
            if (ex.InnerException != null)
            {
                _logger.LogError("{ExceptionType} {ExceptionMessage}", ex.InnerException.GetType().ToString(), ex.InnerException.ToString());
            }
            else
            {
                _logger.LogError("{ExceptionType} {ExceptionMessage}", ex.GetType().ToString(), ex.ToString());
            }
            // context.Response.StatusCode = 500;
            // await context.Response.WriteAsync("Error Occured srinathuuuuuuu");
            throw;
        }
    }
}

public static class MyCustomMiddleware
{
    public static IApplicationBuilder  UseGlobalExcpetionMiddleWare(this IApplicationBuilder app)
    {
        return app.UseMiddleware<GlobalExceptionHandler>();
    }
}
