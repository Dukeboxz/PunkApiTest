using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PunkApi_Data.Migrations
{
    public partial class linkTables3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BeerUserFavourites");

            migrationBuilder.RenameColumn(
                name: "BeerId",
                table: "Beers",
                newName: "ApiId");

            migrationBuilder.AddColumn<int>(
                name: "BeerId",
                table: "UserFavourites",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "UserBeers",
                columns: table => new
                {
                    UserFavouritesId = table.Column<int>(type: "int", nullable: false),
                    BeerId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserBeers", x => new { x.UserFavouritesId, x.BeerId });
                    table.ForeignKey(
                        name: "FK_UserBeers_Beers_BeerId",
                        column: x => x.BeerId,
                        principalTable: "Beers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserBeers_UserFavourites_UserFavouritesId",
                        column: x => x.UserFavouritesId,
                        principalTable: "UserFavourites",
                        principalColumn: "UserFavouritesId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_UserFavourites_BeerId",
                table: "UserFavourites",
                column: "BeerId");

            migrationBuilder.CreateIndex(
                name: "IX_UserBeers_BeerId",
                table: "UserBeers",
                column: "BeerId");

            migrationBuilder.AddForeignKey(
                name: "FK_UserFavourites_Beers_BeerId",
                table: "UserFavourites",
                column: "BeerId",
                principalTable: "Beers",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserFavourites_Beers_BeerId",
                table: "UserFavourites");

            migrationBuilder.DropTable(
                name: "UserBeers");

            migrationBuilder.DropIndex(
                name: "IX_UserFavourites_BeerId",
                table: "UserFavourites");

            migrationBuilder.DropColumn(
                name: "BeerId",
                table: "UserFavourites");

            migrationBuilder.RenameColumn(
                name: "ApiId",
                table: "Beers",
                newName: "BeerId");

            migrationBuilder.CreateTable(
                name: "BeerUserFavourites",
                columns: table => new
                {
                    FavouritesId = table.Column<int>(type: "int", nullable: false),
                    UserFavouritesId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BeerUserFavourites", x => new { x.FavouritesId, x.UserFavouritesId });
                    table.ForeignKey(
                        name: "FK_BeerUserFavourites_Beers_FavouritesId",
                        column: x => x.FavouritesId,
                        principalTable: "Beers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BeerUserFavourites_UserFavourites_UserFavouritesId",
                        column: x => x.UserFavouritesId,
                        principalTable: "UserFavourites",
                        principalColumn: "UserFavouritesId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_BeerUserFavourites_UserFavouritesId",
                table: "BeerUserFavourites",
                column: "UserFavouritesId");
        }
    }
}
