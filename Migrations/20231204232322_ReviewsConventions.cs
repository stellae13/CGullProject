using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CGullProject.Migrations
{
    /// <inheritdoc />
    public partial class ReviewsConventions : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_reviews_Cart_CartId",
                table: "reviews");

            migrationBuilder.DropForeignKey(
                name: "FK_reviews_Inventory_ProductId",
                table: "reviews");

            migrationBuilder.DropPrimaryKey(
                name: "PK_reviews",
                table: "reviews");

            migrationBuilder.RenameTable(
                name: "reviews",
                newName: "Reviews");

            migrationBuilder.RenameColumn(
                name: "rating",
                table: "Reviews",
                newName: "Rating");

            migrationBuilder.RenameColumn(
                name: "lastUpdated",
                table: "Reviews",
                newName: "LastUpdated");

            migrationBuilder.RenameColumn(
                name: "comment",
                table: "Reviews",
                newName: "Comment");

            migrationBuilder.RenameColumn(
                name: "ProductId",
                table: "Reviews",
                newName: "ItemId");

            migrationBuilder.RenameIndex(
                name: "IX_reviews_ProductId",
                table: "Reviews",
                newName: "IX_Reviews_ItemId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Reviews",
                table: "Reviews",
                columns: new[] { "CartId", "InventoryId" });

            migrationBuilder.AddForeignKey(
                name: "FK_Reviews_Cart_CartId",
                table: "Reviews",
                column: "CartId",
                principalTable: "Cart",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Reviews_Inventory_ItemId",
                table: "Reviews",
                column: "ItemId",
                principalTable: "Inventory",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Reviews_Cart_CartId",
                table: "Reviews");

            migrationBuilder.DropForeignKey(
                name: "FK_Reviews_Inventory_ItemId",
                table: "Reviews");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Reviews",
                table: "Reviews");

            migrationBuilder.RenameTable(
                name: "Reviews",
                newName: "reviews");

            migrationBuilder.RenameColumn(
                name: "Rating",
                table: "reviews",
                newName: "rating");

            migrationBuilder.RenameColumn(
                name: "LastUpdated",
                table: "reviews",
                newName: "lastUpdated");

            migrationBuilder.RenameColumn(
                name: "Comment",
                table: "reviews",
                newName: "comment");

            migrationBuilder.RenameColumn(
                name: "ItemId",
                table: "reviews",
                newName: "ProductId");

            migrationBuilder.RenameIndex(
                name: "IX_Reviews_ItemId",
                table: "reviews",
                newName: "IX_reviews_ProductId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_reviews",
                table: "reviews",
                columns: new[] { "CartId", "InventoryId" });

            migrationBuilder.AddForeignKey(
                name: "FK_reviews_Cart_CartId",
                table: "reviews",
                column: "CartId",
                principalTable: "Cart",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_reviews_Inventory_ProductId",
                table: "reviews",
                column: "ProductId",
                principalTable: "Inventory",
                principalColumn: "Id");
        }
    }
}
