using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EmployeeBenefitsWorkflow.Infrastructure.Persistence.Migrations;

/// <inheritdoc />
public partial class _20261123110447_changeAllRequestProcedure : Migration
{
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        string procedureSql=@"ALTER PROCEDURE [dbo].[GetAllRequest] AS
        BEGIN
            SET NOCOUNT ON;
            SELECT RequestId,EmployeeId,CourseName,ClaimExpense,RequestStatus FROM [dbo].[ReimbursementRequests]
        END
        ";
        migrationBuilder.Sql(procedureSql);
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {
        string procedureSql=@"ALTER PROCEDURE [dbo].[GetAllRequest] AS
        BEGIN
            SET NOCOUNT ON;
            SELECT RequestId,EmployeeId,CourseName,ClaimAmount,RequestStatus FROM [dbo].[ReimbursementRequests]
        END
        ";
        migrationBuilder.Sql(procedureSql);
    }
}
