using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EmployeeBenefitsWorkflow.Infrastructure.Persistence.Migrations;

/// <inheritdoc />
public partial class _20261123111827_indexForRequestIdAndConstraints : Migration
{
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.CreateIndex(
            name: "IX_ReimbursementRequests_RequestId",
            schema: "dbo",
            table: "ReimbursementRequests",
            column: "RequestId",
            unique: true);

        migrationBuilder.AddCheckConstraint(
            name: "chk_claimExpense",
            schema: "dbo",
            table: "ReimbursementRequests",
            sql: "[ClaimExpense]>=0.0 AND [ClaimExpense]<=10000.00");
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropIndex(
            name: "IX_ReimbursementRequests_RequestId",
            schema: "dbo",
            table: "ReimbursementRequests");

        migrationBuilder.DropCheckConstraint(
            name: "chk_claimExpense",
            schema: "dbo",
            table: "ReimbursementRequests");
    }
}
