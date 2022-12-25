using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ThatSneakerShopLaced.Data.Migrations
{
    public partial class ChangesWishlist : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ShoeId",
                table: "Wishlist",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Wishlist_ShoeId",
                table: "Wishlist",
                column: "ShoeId");

            migrationBuilder.AddForeignKey(
                name: "FK_Wishlist_Shoe_ShoeId",
                table: "Wishlist",
                column: "ShoeId",
                principalTable: "Shoe",
                principalColumn: "ShoeId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Wishlist_Shoe_ShoeId",
                table: "Wishlist");

            migrationBuilder.DropIndex(
                name: "IX_Wishlist_ShoeId",
                table: "Wishlist");

            migrationBuilder.DropColumn(
                name: "ShoeId",
                table: "Wishlist");
        }
    }
}
