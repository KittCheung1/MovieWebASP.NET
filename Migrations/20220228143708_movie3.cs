using Microsoft.EntityFrameworkCore.Migrations;

namespace TestWebASP.NET.Migrations
{
    public partial class movie3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Characters",
                columns: new[] { "Id", "Alias", "FullName", "Gender", "Picture" },
                values: new object[,]
                {
                    { 2, "Iron Man", "Tony Stark", "Male", null },
                    { 3, "Spiderman", "Peter Parker", "Male", null }
                });

            migrationBuilder.InsertData(
                table: "Franchises",
                columns: new[] { "Id", "Description", "Name" },
                values: new object[] { 2, null, "Marvel" });

            migrationBuilder.InsertData(
                table: "Movies",
                columns: new[] { "Id", "Director", "FranchiseId", "Genre", "MovieTitle", "Picture", "ReleaseYear", "Trailer" },
                values: new object[,]
                {
                    { 2, null, 2, null, "Iron Man", null, 0, null },
                    { 3, null, 2, null, "Iron Man 2", null, 0, null }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Characters",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Characters",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Franchises",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Movies",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Movies",
                keyColumn: "Id",
                keyValue: 3);
        }
    }
}
