using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace EmployeeBenefitsWorkflow.Infrastructure.Persistence.Migrations;

/// <inheritdoc />
public partial class _20261117185336_seed_data_into_users : Migration
{
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.InsertData(
            table: "Users",
            columns: new[] { "Id", "Email", "Name", "Role" },
            values: new object[,]
            {
                { new Guid("f3c7a1d2-4b5e-4c8a-9d2f-1a2b3c4d5e01"), "srinath@example.com", "Srinath", 0 },
                { new Guid("f3c7a1d2-4b5e-4c8a-9d2f-1a2b3c4d5e02"), "sathvika@example.com", "Sathvika", 0 },
                { new Guid("f3c7a1d2-4b5e-4c8a-9d2f-1a2b3c4d5e03"), "meghana@example.com", "Meghana", 1 },
                { new Guid("f3c7a1d2-4b5e-4c8a-9d2f-1a2b3c4d5e04"), "sathya@example.com", "Sathya", 1 }
            });
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DeleteData(
            table: "Users",
            keyColumn: "Id",
            keyValue: new Guid("f3c7a1d2-4b5e-4c8a-9d2f-1a2b3c4d5e01"));

        migrationBuilder.DeleteData(
            table: "Users",
            keyColumn: "Id",
            keyValue: new Guid("f3c7a1d2-4b5e-4c8a-9d2f-1a2b3c4d5e02"));

        migrationBuilder.DeleteData(
            table: "Users",
            keyColumn: "Id",
            keyValue: new Guid("f3c7a1d2-4b5e-4c8a-9d2f-1a2b3c4d5e03"));

        migrationBuilder.DeleteData(
            table: "Users",
            keyColumn: "Id",
            keyValue: new Guid("f3c7a1d2-4b5e-4c8a-9d2f-1a2b3c4d5e04"));
    }
}
