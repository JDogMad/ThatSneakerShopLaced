using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ThatSneakerShopLaced.Data.Migrations
{
    public partial class ChangesToShoe : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ImageUrl",
                table: "Shoe",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ImageUrl",
                table: "Shoe");
        }
    }
}
