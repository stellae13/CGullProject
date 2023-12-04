using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CGullProject.Migrations
{
    /// <inheritdoc />
    public partial class SetSHA256AdminPasswdColumn : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_reviews_Inventory_ProductId",
                table: "reviews");

            migrationBuilder.RenameColumn(
                name: "ProductId",
                table: "reviews",
                newName: "ItemId");

            migrationBuilder.RenameIndex(
                name: "IX_reviews_ProductId",
                table: "reviews",
                newName: "IX_reviews_ItemId");

            migrationBuilder.AlterColumn<string>(
                name: "Password",
                table: "Admins",
                type: "varchar(32)",
                unicode: false,
                maxLength: 32,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddForeignKey(
                name: "FK_reviews_Inventory_ItemId",
                table: "reviews",
                column: "ItemId",
                principalTable: "Inventory",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_reviews_Inventory_ItemId",
                table: "reviews");

            migrationBuilder.RenameColumn(
                name: "ItemId",
                table: "reviews",
                newName: "ProductId");

            migrationBuilder.RenameIndex(
                name: "IX_reviews_ItemId",
                table: "reviews",
                newName: "IX_reviews_ProductId");

            migrationBuilder.AlterColumn<string>(
                name: "Password",
                table: "Admins",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(32)",
                oldUnicode: false,
                oldMaxLength: 32);

            migrationBuilder.AddForeignKey(
                name: "FK_reviews_Inventory_ProductId",
                table: "reviews",
                column: "ProductId",
                principalTable: "Inventory",
                principalColumn: "Id");
        }
    }
}
