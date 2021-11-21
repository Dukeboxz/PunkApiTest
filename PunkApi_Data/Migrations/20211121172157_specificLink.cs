using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PunkApi_Data.Migrations
{
    public partial class specificLink : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserFavourites_Beers_BeerId",
                table: "UserFavourites");

            migrationBuilder.DropIndex(
                name: "IX_UserFavourites_BeerId",
                table: "UserFavourites");

            migrationBuilder.DropColumn(
                name: "BeerId",
                table: "UserFavourites");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Beers",
                newName: "BeerId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "BeerId",
                table: "Beers",
                newName: "Id");

            migrationBuilder.AddColumn<int>(
                name: "BeerId",
                table: "UserFavourites",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_UserFavourites_BeerId",
                table: "UserFavourites",
                column: "BeerId");

            migrationBuilder.AddForeignKey(
                name: "FK_UserFavourites_Beers_BeerId",
                table: "UserFavourites",
                column: "BeerId",
                principalTable: "Beers",
                principalColumn: "Id");
        }
    }
}
