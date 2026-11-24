namespace EmployeeBenefitsWorkflow.Application.Exceptions;

public abstract class ServiceException(string message, int statusCode) : Exception(message)
{
    public int StatusCode { get; } = statusCode;
}

public sealed class BadRequestException(string message) : ServiceException(message, 400);

public sealed class NotFoundException(string message) : ServiceException(message, 404);

public sealed class ConflictException(string message) : ServiceException(message, 409);

public sealed class InternalServerException(string message) : ServiceException(message, 500);

public sealed class FeatureNotImplementedException(string message) : ServiceException(message, 501);
