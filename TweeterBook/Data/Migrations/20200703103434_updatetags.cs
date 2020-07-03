using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TweeterBook.Data.Migrations
{
    public partial class updatetags : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Employees",
                keyColumn: "Id",
                keyValue: new Guid("68e62788-1121-40e1-ab4a-36568d4f3d18"));

            migrationBuilder.CreateTable(
                name: "Tags",
                columns: table => new
                {
                    Name = table.Column<string>(nullable: false),
                    CreatorId = table.Column<string>(nullable: true),
                    CreatedOn = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tags", x => x.Name);
                    table.ForeignKey(
                        name: "FK_Tags_AspNetUsers_CreatorId",
                        column: x => x.CreatorId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "EmployeeTags",
                columns: table => new
                {
                    TagName = table.Column<string>(nullable: false),
                    EmpId = table.Column<Guid>(nullable: false),
                    EmployeeId = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmployeeTags", x => new { x.EmpId, x.TagName });
                    table.ForeignKey(
                        name: "FK_EmployeeTags_Employees_EmployeeId",
                        column: x => x.EmployeeId,
                        principalTable: "Employees",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_EmployeeTags_Tags_TagName",
                        column: x => x.TagName,
                        principalTable: "Tags",
                        principalColumn: "Name",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_EmployeeTags_EmployeeId",
                table: "EmployeeTags",
                column: "EmployeeId");

            migrationBuilder.CreateIndex(
                name: "IX_EmployeeTags_TagName",
                table: "EmployeeTags",
                column: "TagName");

            migrationBuilder.CreateIndex(
                name: "IX_Tags_CreatorId",
                table: "Tags",
                column: "CreatorId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "EmployeeTags");

            migrationBuilder.DropTable(
                name: "Tags");

            migrationBuilder.InsertData(
                table: "Employees",
                columns: new[] { "Id", "Name", "UserId" },
                values: new object[] { new Guid("68e62788-1121-40e1-ab4a-36568d4f3d18"), "kalai", null });
        }
    }
}
