using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PunkApi_Data.Migrations
{
    public partial class CreateDatabase : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "UserFavourites",
                columns: table => new
                {
                    UserFavouritesId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserID = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserFavourites", x => x.UserFavouritesId);
                });

            migrationBuilder.CreateTable(
                name: "Beers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Tagline = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FirstBrewed = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ImageUrl = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Abv = table.Column<double>(type: "float", nullable: false),
                    Ibu = table.Column<int>(type: "int", nullable: false),
                    TargetFg = table.Column<int>(type: "int", nullable: false),
                    TargetOg = table.Column<int>(type: "int", nullable: false),
                    Ebc = table.Column<int>(type: "int", nullable: false),
                    Srm = table.Column<int>(type: "int", nullable: false),
                    Ph = table.Column<double>(type: "float", nullable: false),
                    AttenuationLevel = table.Column<int>(type: "int", nullable: false),
                    BrewersTips = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ContributedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserFavouritesId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Beers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Beers_UserFavourites_UserFavouritesId",
                        column: x => x.UserFavouritesId,
                        principalTable: "UserFavourites",
                        principalColumn: "UserFavouritesId");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Beers_UserFavouritesId",
                table: "Beers",
                column: "UserFavouritesId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Beers");

            migrationBuilder.DropTable(
                name: "UserFavourites");
        }
    }
}
