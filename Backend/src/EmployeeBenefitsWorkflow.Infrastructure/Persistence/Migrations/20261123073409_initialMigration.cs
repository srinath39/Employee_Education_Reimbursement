using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace EmployeeBenefitsWorkflow.Infrastructure.Persistence.Migrations;

/// <inheritdoc />
public partial class _20261123073409_initialMigration : Migration
{
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.CreateTable(
            name: "Users",
            columns: table => new
            {
                Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                Role = table.Column<int>(type: "int", nullable: false),
                Email = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_Users", x => x.Id);
            });

        migrationBuilder.CreateTable(
            name: "ReimbursementRequests",
            columns: table => new
            {
                RequestId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                EmployeeId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                CourseName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                ClaimAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                RequestStatus = table.Column<int>(type: "int", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_ReimbursementRequests", x => x.RequestId);
                table.ForeignKey(
                    name: "FK_ReimbursementRequests_Users_EmployeeId",
                    column: x => x.EmployeeId,
                    principalTable: "Users",
                    principalColumn: "Id",
                    onDelete: ReferentialAction.Restrict);
            });

        migrationBuilder.CreateTable(
            name: "RequestDecisions",
            columns: table => new
            {
                DecisionId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                RequestId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                HrId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                RequestStatus = table.Column<int>(type: "int", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_RequestDecisions", x => x.DecisionId);
                table.ForeignKey(
                    name: "FK_RequestDecisions_ReimbursementRequests_RequestId",
                    column: x => x.RequestId,
                    principalTable: "ReimbursementRequests",
                    principalColumn: "RequestId",
                    onDelete: ReferentialAction.Restrict);
                table.ForeignKey(
                    name: "FK_RequestDecisions_Users_HrId",
                    column: x => x.HrId,
                    principalTable: "Users",
                    principalColumn: "Id",
                    onDelete: ReferentialAction.Restrict);
            });

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

        migrationBuilder.CreateIndex(
            name: "IX_ReimbursementRequests_EmployeeId",
            table: "ReimbursementRequests",
            column: "EmployeeId");

        migrationBuilder.CreateIndex(
            name: "IX_RequestDecisions_HrId",
            table: "RequestDecisions",
            column: "HrId");

        migrationBuilder.CreateIndex(
            name: "IX_RequestDecisions_RequestId",
            table: "RequestDecisions",
            column: "RequestId",
            unique: true);
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropTable(
            name: "RequestDecisions");

        migrationBuilder.DropTable(
            name: "ReimbursementRequests");

        migrationBuilder.DropTable(
            name: "Users");
    }
}
