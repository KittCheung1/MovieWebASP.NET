using Microsoft.EntityFrameworkCore.Migrations;

namespace TestWebASP.NET.Migrations
{
    public partial class added_franchiseid : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Movies",
                keyColumn: "Id",
                keyValue: 1,
                column: "FranchiseId",
                value: 1);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Movies",
                keyColumn: "Id",
                keyValue: 1,
                column: "FranchiseId",
                value: 0);
        }
    }
}
