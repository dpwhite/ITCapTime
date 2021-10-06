using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ITCapTime.Data.Migrations
{
    public partial class ProjectEmployeeChange : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProjectEmployees_Employees_EmployeesId",
                table: "ProjectEmployees");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ProjectEmployees",
                table: "ProjectEmployees");

            migrationBuilder.DropColumn(
                name: "EmployeesId",
                table: "ProjectEmployees");

            migrationBuilder.RenameColumn(
                name: "EmployeerId",
                table: "ProjectEmployees",
                newName: "EmployeeId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ProjectEmployees",
                table: "ProjectEmployees",
                columns: new[] { "EmployeeId", "ProjectId" });

            migrationBuilder.AddForeignKey(
                name: "FK_ProjectEmployees_Employees_EmployeeId",
                table: "ProjectEmployees",
                column: "EmployeeId",
                principalTable: "Employees",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProjectEmployees_Employees_EmployeeId",
                table: "ProjectEmployees");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ProjectEmployees",
                table: "ProjectEmployees");

            migrationBuilder.RenameColumn(
                name: "EmployeeId",
                table: "ProjectEmployees",
                newName: "EmployeerId");

            migrationBuilder.AddColumn<Guid>(
                name: "EmployeesId",
                table: "ProjectEmployees",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddPrimaryKey(
                name: "PK_ProjectEmployees",
                table: "ProjectEmployees",
                columns: new[] { "EmployeesId", "ProjectId" });

            migrationBuilder.AddForeignKey(
                name: "FK_ProjectEmployees_Employees_EmployeesId",
                table: "ProjectEmployees",
                column: "EmployeesId",
                principalTable: "Employees",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
