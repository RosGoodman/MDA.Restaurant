using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Common.Migrations
{
    public partial class _002 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Tables_Restaurants_RestaurantId",
                table: "Tables");

            migrationBuilder.DropIndex(
                name: "IX_Tables_RestaurantId",
                table: "Tables");

            migrationBuilder.AddColumn<int>(
                name: "RestaurantModelId",
                table: "Tables",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "SeatsCount",
                table: "Tables",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "Restaurants",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_Tables_RestaurantModelId",
                table: "Tables",
                column: "RestaurantModelId");

            migrationBuilder.AddForeignKey(
                name: "FK_Tables_Restaurants_RestaurantModelId",
                table: "Tables",
                column: "RestaurantModelId",
                principalTable: "Restaurants",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Tables_Restaurants_RestaurantModelId",
                table: "Tables");

            migrationBuilder.DropIndex(
                name: "IX_Tables_RestaurantModelId",
                table: "Tables");

            migrationBuilder.DropColumn(
                name: "RestaurantModelId",
                table: "Tables");

            migrationBuilder.DropColumn(
                name: "SeatsCount",
                table: "Tables");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "Restaurants");

            migrationBuilder.CreateIndex(
                name: "IX_Tables_RestaurantId",
                table: "Tables",
                column: "RestaurantId");

            migrationBuilder.AddForeignKey(
                name: "FK_Tables_Restaurants_RestaurantId",
                table: "Tables",
                column: "RestaurantId",
                principalTable: "Restaurants",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
