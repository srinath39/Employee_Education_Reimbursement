using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EmployeeBenefitsWorkflow.Infrastructure.Persistence.Migrations;

/// <inheritdoc />
public partial class _20261123073733_getAllRequestMigration : Migration
{
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        string procedureSql=@"CREATE PROCEDURE [dbo].[GetAllRequest] AS
        BEGIN
            SELECT RequestId,EmployeeId,CourseName,ClaimAmount,RequestStatus FROM [dbo].[ReimbursementRequests]
        END
        ";
        migrationBuilder.Sql(procedureSql);
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {
        string procedureSql=@"DROP PROCEDURE [dbo].[GetAllRequest]";
        migrationBuilder.Sql(procedureSql);
    }
}
