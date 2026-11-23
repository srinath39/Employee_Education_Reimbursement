using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EmployeeBenefitsWorkflow.Infrastructure.Persistence.Migrations;

/// <inheritdoc />
public partial class _20261123085649_UpdateUserProcedure : Migration
{
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        string updateUserSp = @"CREATE PROCEDURE [dbo].[updateUser]
        (@UserName nvarchar(100),@UserEmail nvarchar(100))
         AS
        BEGIN
            UPDATE [dbo].[Users] SET NAME=@UserName WHERE EMAIL=@UserEmail
        END
        ";
        migrationBuilder.Sql(updateUserSp);
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {
        string updateUserSp = @"DROP PROCEDURE [dbo].[updateUser]";
        migrationBuilder.Sql(updateUserSp);
    }
}
