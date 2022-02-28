using Microsoft.EntityFrameworkCore.Migrations;

namespace TestWebASP.NET.Migrations
{
    public partial class movie : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "CharacterMovie",
                columns: new[] { "CharactersId", "MoviesId" },
                values: new object[] { 1, 1 });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "CharacterMovie",
                keyColumns: new[] { "CharactersId", "MoviesId" },
                keyValues: new object[] { 1, 1 });
        }
    }
}
