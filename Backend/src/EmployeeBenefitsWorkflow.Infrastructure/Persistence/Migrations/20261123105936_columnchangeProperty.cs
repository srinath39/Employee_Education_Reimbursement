using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EmployeeBenefitsWorkflow.Infrastructure.Persistence.Migrations;

/// <inheritdoc />
public partial class _20261123105936_columnchangeProperty : Migration
{
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {

        migrationBuilder.RenameColumn(
            name: "ClaimAmount",
            schema: "dbo",
            table: "ReimbursementRequests",
            newName: "ClaimExpense");

        migrationBuilder.AlterColumn<decimal>(
            name: "ClaimExpense",
            schema: "dbo",
            table: "ReimbursementRequests",
            type: "decimal(10,2)",
            nullable: false,
            defaultValue: 0.0m,
            oldClrType: typeof(decimal),
            oldType: "decimal(18,2)");
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {

        migrationBuilder.RenameColumn(
            name: "ClaimExpense",
            table: "ReimbursementRequests",
            newName: "ClaimAmount");

        migrationBuilder.AlterColumn<decimal>(
            name: "ClaimAmount",
            table: "ReimbursementRequests",
            type: "decimal(18,2)",
            nullable: false,
            oldClrType: typeof(decimal),
            oldType: "decimal(10,2)",
            oldDefaultValue: 0.0m);
    }
}
