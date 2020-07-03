using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TweeterBook.Data.Migrations
{
    public partial class useridupdate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Employees",
                keyColumn: "Id",
                keyValue: new Guid("5cd222c3-210c-40dc-bddf-d9239f5158d9"));

            migrationBuilder.InsertData(
                table: "Employees",
                columns: new[] { "Id", "Name", "UserId" },
                values: new object[] { new Guid("deb91211-091a-4837-8ab8-93cfcdd9e88b"), "kalai", null });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Employees",
                keyColumn: "Id",
                keyValue: new Guid("deb91211-091a-4837-8ab8-93cfcdd9e88b"));

            migrationBuilder.InsertData(
                table: "Employees",
                columns: new[] { "Id", "Name", "UserId" },
                values: new object[] { new Guid("5cd222c3-210c-40dc-bddf-d9239f5158d9"), "kalai", null });
        }
    }
}
