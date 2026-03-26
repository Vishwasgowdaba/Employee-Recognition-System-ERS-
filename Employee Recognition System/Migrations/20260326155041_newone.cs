using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Employee_Recogition_System.Migrations
{
    /// <inheritdoc />
    public partial class newone : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Appreciations",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SenderId = table.Column<int>(type: "int", nullable: false),
                    ReceiverId = table.Column<int>(type: "int", nullable: false),
                    Message = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Appreciations", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AwardCategories",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Points = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AwardCategories", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Employees",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Points = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Employees", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Nominations",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EmployeeId = table.Column<int>(type: "int", nullable: false),
                    AwardCategoryId = table.Column<int>(type: "int", nullable: false),
                    NominatedById = table.Column<int>(type: "int", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Nominations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Nominations_AwardCategories_AwardCategoryId",
                        column: x => x.AwardCategoryId,
                        principalTable: "AwardCategories",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Nominations_Employees_EmployeeId",
                        column: x => x.EmployeeId,
                        principalTable: "Employees",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Nominations_Employees_NominatedById",
                        column: x => x.NominatedById,
                        principalTable: "Employees",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Nominations_AwardCategoryId",
                table: "Nominations",
                column: "AwardCategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Nominations_EmployeeId",
                table: "Nominations",
                column: "EmployeeId");

            migrationBuilder.CreateIndex(
                name: "IX_Nominations_NominatedById",
                table: "Nominations",
                column: "NominatedById");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Appreciations");

            migrationBuilder.DropTable(
                name: "Nominations");

            migrationBuilder.DropTable(
                name: "AwardCategories");

            migrationBuilder.DropTable(
                name: "Employees");
        }
    }
}
