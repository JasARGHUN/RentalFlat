using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DataAccess.Migrations
{
    public partial class AddOrderDetails : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "HomeImages");

            migrationBuilder.CreateTable(
                name: "OrderDetails",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    StripeSessionId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CheckInDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CheckOutDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ActualCheckInDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ActualCheckOutDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    TotalCost = table.Column<double>(type: "float", nullable: false),
                    FlatId = table.Column<int>(type: "int", nullable: false),
                    IsPaymentSuccessful = table.Column<bool>(type: "bit", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Phone = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderDetails", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OrderDetails_Flats_FlatId",
                        column: x => x.FlatId,
                        principalTable: "Flats",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_OrderDetails_FlatId",
                table: "OrderDetails",
                column: "FlatId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "OrderDetails");

            migrationBuilder.CreateTable(
                name: "HomeImages",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    HomeId = table.Column<int>(type: "int", nullable: false),
                    HomeImageUrl = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HomeImages", x => x.Id);
                    table.ForeignKey(
                        name: "FK_HomeImages_HomepageInfos_HomeId",
                        column: x => x.HomeId,
                        principalTable: "HomepageInfos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_HomeImages_HomeId",
                table: "HomeImages",
                column: "HomeId");
        }
    }
}
