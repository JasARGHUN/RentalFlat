using Microsoft.EntityFrameworkCore.Migrations;

namespace DataAccess.Migrations
{
    public partial class FixHomepageModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_HomepageImages_HomepageInfoId",
                table: "HomepageImages");

            migrationBuilder.CreateIndex(
                name: "IX_HomepageImages_HomepageInfoId",
                table: "HomepageImages",
                column: "HomepageInfoId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_HomepageImages_HomepageInfoId",
                table: "HomepageImages");

            migrationBuilder.CreateIndex(
                name: "IX_HomepageImages_HomepageInfoId",
                table: "HomepageImages",
                column: "HomepageInfoId",
                unique: true);
        }
    }
}
