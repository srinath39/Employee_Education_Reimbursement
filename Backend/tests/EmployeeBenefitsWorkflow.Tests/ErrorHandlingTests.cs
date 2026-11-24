using EmployeeBenefitsWorkflow.Api.Controllers;
using EmployeeBenefitsWorkflow.Application.DTOs;
using EmployeeBenefitsWorkflow.Application.Exceptions;
using EmployeeBenefitsWorkflow.Application.interfaces.Repository;
using EmployeeBenefitsWorkflow.Application.service;
using EmployeeBenefitsWorkflow.Domain.Entities;
using EmployeeBenefitsWorkflow.Domain.Enums;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Xunit;

namespace EmployeeBenefitsWorkflow.Tests;

public class ErrorHandlingTests
{
    [Fact]
    public async Task MissingRequestAndUserProduceNotFound()
    {
        var users = new Users();
        var service = new RequestService(new Requests(), new Decisions(), users);

        await Assert.ThrowsAsync<NotFoundException>(() => service.GetRequestById(Guid.NewGuid()));
        await Assert.ThrowsAsync<NotFoundException>(() =>
            new UserAuthService(users).updateUserName(Guid.NewGuid(), "Alice"));
    }

    [Fact]
    public async Task InvalidInputIsRejected()
    {
        var service = new RequestService(new Requests(), new Decisions(), new Users());

        await Assert.ThrowsAsync<BadRequestException>(() => service.GetEmployeeStatistics(Guid.Empty));
        await Assert.ThrowsAsync<BadRequestException>(() => service.CreateRequest(
            Guid.NewGuid(), new RequestReimbursmentDto { CourseName = "Course", ClaimAmount = 100 }));
        await Assert.ThrowsAsync<BadRequestException>(() =>
            new UserAuthService(new Users()).updateUserName(Guid.NewGuid(), new string('a', 101)));
    }

    [Theory]
    [InlineData(true)]
    [InlineData(false)]
    public async Task DecidedRequestCannotBeChanged(bool approve)
    {
        var requests = new Requests
        {
            Result = new ReimbursementRequest { RequestStatus = EnumRequestStatus.APPROVED }
        };
        var decisions = new Decisions();
        var service = new RequestService(requests, decisions, new Users { Result = new User() });
        Func<Task> action = approve
            ? () => service.ApproveRequest(Guid.NewGuid(), Guid.NewGuid())
            : () => service.RejectRequest(Guid.NewGuid(), Guid.NewGuid());

        await Assert.ThrowsAsync<ConflictException>(action);
        Assert.False(requests.Saved);
        Assert.False(decisions.Created);
        Assert.Equal(EnumRequestStatus.APPROVED, requests.Result.RequestStatus);
    }

    [Theory]
    [InlineData(true)]
    [InlineData(false)]
    public async Task UnimplementedAuthReturns501(bool login)
    {
        var service = new UserAuthService(new Users());
        Func<Task<ResponseUserDto>> action = login
            ? () => service.UserLogin(new RequestUserDto())
            : () => service.RegisterUser(new RequestUserDto());
        var exception = await Assert.ThrowsAsync<FeatureNotImplementedException>(action);
        var response = Handle(exception);

        Assert.Equal(501, response.Status);
        Assert.Equal(exception.Message, response.Detail);
    }

    [Fact]
    public void UnexpectedErrorsAreNotExposed()
    {
        var response = Handle(new InvalidOperationException("private database detail"));

        Assert.Equal(500, response.Status);
        Assert.Equal("An unexpected error occurred.", response.Detail);
        Assert.Equal("/original/path", response.Instance);
    }

    private static ProblemDetails Handle(Exception error)
    {
        var context = new DefaultHttpContext();
        context.Features.Set<IExceptionHandlerPathFeature>(new ExceptionHandlerFeature
        {
            Error = error,
            Path = "/original/path"
        });
        var controller = new ErrorController
        {
            ControllerContext = new ControllerContext { HttpContext = context }
        };
        var result = Assert.IsType<ObjectResult>(controller.GetErrorResponse());
        var problem = Assert.IsType<ProblemDetails>(result.Value);

        Assert.Equal(problem.Status, result.StatusCode);
        return problem;
    }

    private sealed class Users : IUserRepository
    {
        public User? Result { get; init; }

        public Task<User?> GetUserByUserId(Guid userId) => Task.FromResult(Result);

        public Task<int> UpdateUserByUserId(Guid userId, string updatedName) => Task.FromResult(0);
    }

    private sealed class Requests : IRequestRepository
    {
        public ReimbursementRequest? Result { get; init; }
        public bool Saved { get; private set; }

        public Task<ReimbursementRequest?> GetRequestByRequestId(Guid requestId) => Task.FromResult(Result);

        public Task SaveRequestChanges()
        {
            Saved = true;
            return Task.CompletedTask;
        }

        public Task<Guid> CreateRequest(ReimbursementRequest request) => throw new NotImplementedException();

        public Task<List<ReimbursementRequest>> GetAllRequestByEmployeeId(Guid empId) => throw new NotImplementedException();

        public Task<List<ReimbursementRequest>> GetAllRequestByRequestStatus(EnumRequestStatus status) => throw new NotImplementedException();
    }

    private sealed class Decisions : IRequestDecisionRepository
    {
        public bool Created { get; private set; }

        public Task CreateRequestDecision(RequestDecisions requestDecision)
        {
            Created = true;
            return Task.CompletedTask;
        }

        public Task SaveRequestDecisionsChanges() => Task.CompletedTask;

        public Task<List<RequestDecisions>> GetRequestDecisionByHrId(Guid hrId) => throw new NotImplementedException();

        public Task<List<RequestDecisions>> GetAllRequestDecisionsByRequestStatus(EnumRequestStatus status) => throw new NotImplementedException();
    }
}
