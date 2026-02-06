using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace ShoppingCart.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddIdPrimaryKeyToCartItems : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_cart_items",
                table: "cart_items");

            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "cart_items",
                type: "integer",
                nullable: false,
                defaultValue: 0)
                .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            migrationBuilder.AddPrimaryKey(
                name: "PK_cart_items",
                table: "cart_items",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_cart_items_CartId_ArticleId",
                table: "cart_items",
                columns: new[] { "CartId", "ArticleId" },
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_cart_items",
                table: "cart_items");

            migrationBuilder.DropIndex(
                name: "IX_cart_items_CartId_ArticleId",
                table: "cart_items");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "cart_items");

            migrationBuilder.AddPrimaryKey(
                name: "PK_cart_items",
                table: "cart_items",
                columns: new[] { "CartId", "ArticleId" });
        }
    }
}
