using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Shopify.Infrastructure.Persistence.Database.Migrations
{
    /// <inheritdoc />
    public partial class FavoriteProduct : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FavoriteProducts_Products_ProductId",
                table: "FavoriteProducts");

            migrationBuilder.DropIndex(
                name: "IX_FavoriteProducts_ProductId",
                table: "FavoriteProducts");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_FavoriteProducts_ProductId",
                table: "FavoriteProducts",
                column: "ProductId");

            migrationBuilder.AddForeignKey(
                name: "FK_FavoriteProducts_Products_ProductId",
                table: "FavoriteProducts",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}