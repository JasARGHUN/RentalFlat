using Microsoft.EntityFrameworkCore.Migrations;

namespace DataAccess.Migrations
{
    public partial class FixHomeImageModels : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_HomeImages_HomepageInfoId",
                table: "HomeImages");

            migrationBuilder.CreateIndex(
                name: "IX_HomeImages_HomepageInfoId",
                table: "HomeImages",
                column: "HomepageInfoId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_HomeImages_HomepageInfoId",
                table: "HomeImages");

            migrationBuilder.CreateIndex(
                name: "IX_HomeImages_HomepageInfoId",
                table: "HomeImages",
                column: "HomepageInfoId",
                unique: true);
        }
    }
}
