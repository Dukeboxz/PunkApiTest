using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PunkApi_Data.Migrations
{
    public partial class linkTables2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "BeerId",
                table: "Beers",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BeerId",
                table: "Beers");
        }
    }
}
