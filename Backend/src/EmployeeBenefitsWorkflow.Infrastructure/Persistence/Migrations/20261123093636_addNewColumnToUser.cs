using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EmployeeBenefitsWorkflow.Infrastructure.Persistence.Migrations;

/// <inheritdoc />
public partial class _20261123093636_addNewColumnToUser : Migration
{
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.AddColumn<string>(
            name: "Tin",
            table: "Users",
            type: "nvarchar(100)",
            maxLength: 100,
            nullable: true);

        migrationBuilder.UpdateData(
            table: "Users",
            keyColumn: "Id",
            keyValue: new Guid("f3c7a1d2-4b5e-4c8a-9d2f-1a2b3c4d5e01"),
            column: "Tin",
            value: null);

        migrationBuilder.UpdateData(
            table: "Users",
            keyColumn: "Id",
            keyValue: new Guid("f3c7a1d2-4b5e-4c8a-9d2f-1a2b3c4d5e02"),
            column: "Tin",
            value: null);

        migrationBuilder.UpdateData(
            table: "Users",
            keyColumn: "Id",
            keyValue: new Guid("f3c7a1d2-4b5e-4c8a-9d2f-1a2b3c4d5e03"),
            column: "Tin",
            value: null);

        migrationBuilder.UpdateData(
            table: "Users",
            keyColumn: "Id",
            keyValue: new Guid("f3c7a1d2-4b5e-4c8a-9d2f-1a2b3c4d5e04"),
            column: "Tin",
            value: null);
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropColumn(
            name: "Tin",
            table: "Users");
    }
}
