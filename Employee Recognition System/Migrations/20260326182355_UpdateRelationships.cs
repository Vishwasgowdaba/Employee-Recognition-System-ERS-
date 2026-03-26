using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Employee_Recogition_System.Migrations
{
    /// <inheritdoc />
    public partial class UpdateRelationships : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Nominations_AwardCategories_AwardCategoryId",
                table: "Nominations");

            migrationBuilder.DropForeignKey(
                name: "FK_Nominations_Employees_EmployeeId",
                table: "Nominations");

            migrationBuilder.DropForeignKey(
                name: "FK_Nominations_Employees_NominatedById",
                table: "Nominations");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "Nominations",
                type: "datetime2",
                nullable: false,
                defaultValueSql: "GETUTCDATE()",
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Employees",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "Email",
                table: "Employees",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "AwardCategories",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "Message",
                table: "Appreciations",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(300)",
                oldMaxLength: 300);

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "Appreciations",
                type: "datetime2",
                nullable: false,
                defaultValueSql: "GETUTCDATE()",
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.CreateIndex(
                name: "IX_Employees_Email",
                table: "Employees",
                column: "Email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Appreciations_ReceiverId",
                table: "Appreciations",
                column: "ReceiverId");

            migrationBuilder.CreateIndex(
                name: "IX_Appreciations_SenderId",
                table: "Appreciations",
                column: "SenderId");

            migrationBuilder.AddForeignKey(
                name: "FK_Appreciations_Employees_ReceiverId",
                table: "Appreciations",
                column: "ReceiverId",
                principalTable: "Employees",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Appreciations_Employees_SenderId",
                table: "Appreciations",
                column: "SenderId",
                principalTable: "Employees",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Nominations_AwardCategories_AwardCategoryId",
                table: "Nominations",
                column: "AwardCategoryId",
                principalTable: "AwardCategories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Nominations_Employees_EmployeeId",
                table: "Nominations",
                column: "EmployeeId",
                principalTable: "Employees",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Nominations_Employees_NominatedById",
                table: "Nominations",
                column: "NominatedById",
                principalTable: "Employees",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Appreciations_Employees_ReceiverId",
                table: "Appreciations");

            migrationBuilder.DropForeignKey(
                name: "FK_Appreciations_Employees_SenderId",
                table: "Appreciations");

            migrationBuilder.DropForeignKey(
                name: "FK_Nominations_AwardCategories_AwardCategoryId",
                table: "Nominations");

            migrationBuilder.DropForeignKey(
                name: "FK_Nominations_Employees_EmployeeId",
                table: "Nominations");

            migrationBuilder.DropForeignKey(
                name: "FK_Nominations_Employees_NominatedById",
                table: "Nominations");

            migrationBuilder.DropIndex(
                name: "IX_Employees_Email",
                table: "Employees");

            migrationBuilder.DropIndex(
                name: "IX_Appreciations_ReceiverId",
                table: "Appreciations");

            migrationBuilder.DropIndex(
                name: "IX_Appreciations_SenderId",
                table: "Appreciations");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "Nominations",
                type: "datetime2",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValueSql: "GETUTCDATE()");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Employees",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100);

            migrationBuilder.AlterColumn<string>(
                name: "Email",
                table: "Employees",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "AwardCategories",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100);

            migrationBuilder.AlterColumn<string>(
                name: "Message",
                table: "Appreciations",
                type: "nvarchar(300)",
                maxLength: 300,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "Appreciations",
                type: "datetime2",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValueSql: "GETUTCDATE()");

            migrationBuilder.AddForeignKey(
                name: "FK_Nominations_AwardCategories_AwardCategoryId",
                table: "Nominations",
                column: "AwardCategoryId",
                principalTable: "AwardCategories",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Nominations_Employees_EmployeeId",
                table: "Nominations",
                column: "EmployeeId",
                principalTable: "Employees",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Nominations_Employees_NominatedById",
                table: "Nominations",
                column: "NominatedById",
                principalTable: "Employees",
                principalColumn: "Id");
        }
    }
}
