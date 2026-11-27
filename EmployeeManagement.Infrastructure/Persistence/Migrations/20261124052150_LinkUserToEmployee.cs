using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EmployeeManagement.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class LinkUserToEmployee : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "EmployeeId",
                table: "UserTable",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_UserTable_EmployeeId",
                table: "UserTable",
                column: "EmployeeId");

            migrationBuilder.AddForeignKey(
                name: "FK_UserTable_EmployeeDetails_EmployeeId",
                table: "UserTable",
                column: "EmployeeId",
                principalTable: "EmployeeDetails",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserTable_EmployeeDetails_EmployeeId",
                table: "UserTable");

            migrationBuilder.DropIndex(
                name: "IX_UserTable_EmployeeId",
                table: "UserTable");

            migrationBuilder.DropColumn(
                name: "EmployeeId",
                table: "UserTable");
        }
    }
}
