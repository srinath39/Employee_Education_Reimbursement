using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EmployeeBenefitsWorkflow.Infrastructure.Persistence.Migrations;

/// <inheritdoc />
public partial class _20261123084147_insertNewUserProcedure : Migration
{
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        string insertNewUserSp=@"CREATE PROCEDURE [dbo].[insertNewUser]
        (@UserName nvarchar(100),@UserRole int,@UserEmail nvarchar(100))
         AS
        BEGIN
            INSERT INTO [dbo].[Users](Name,Role,Email) VALUES (@UserName,@UserRole,@UserEmail)
        END
        ";
        migrationBuilder.Sql(insertNewUserSp);
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {
        string insertNewUserSp=@"DROP PROCEDURE [dbo].[insertNewUser]";
        migrationBuilder.Sql(insertNewUserSp);
    }
}
