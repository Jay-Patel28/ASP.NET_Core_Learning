using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Cinema.Migrations
{
    public partial class addedindexes : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Movies_TotalViews",
                table: "Movies",
                column: "TotalViews");

            migrationBuilder.CreateIndex(
                name: "IX_Actors_wealth",
                table: "Actors",
                column: "wealth");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Movies_TotalViews",
                table: "Movies");

            migrationBuilder.DropIndex(
                name: "IX_Actors_wealth",
                table: "Actors");
        }
    }
}
