using Microsoft.EntityFrameworkCore.Migrations;

namespace DataAccess.Migrations
{
    public partial class AddHomeImage : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Image",
                table: "HomepageInfos");

            migrationBuilder.CreateTable(
                name: "HomeImages",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    HomepageInfoId = table.Column<int>(type: "int", nullable: false),
                    ImageUrl = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HomeImages", x => x.Id);
                    table.ForeignKey(
                        name: "FK_HomeImages_HomepageInfos_HomepageInfoId",
                        column: x => x.HomepageInfoId,
                        principalTable: "HomepageInfos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_HomeImages_HomepageInfoId",
                table: "HomeImages",
                column: "HomepageInfoId",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "HomeImages");

            migrationBuilder.AddColumn<string>(
                name: "Image",
                table: "HomepageInfos",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
