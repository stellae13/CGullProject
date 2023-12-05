using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CGullProject.Migrations
{
    /// <inheritdoc />
    public partial class RenameFromProductToItem : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BundleItem_Inventory_ProductId",
                table: "BundleItem");

            migrationBuilder.DropForeignKey(
                name: "FK_OrderItems_Inventory_ProductId",
                table: "OrderItems");

            migrationBuilder.DropForeignKey(
                name: "FK_Reviews_Inventory_ItemId",
                table: "Reviews");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Reviews",
                table: "Reviews");

            migrationBuilder.DropColumn(
                name: "InventoryId",
                table: "Reviews");

            migrationBuilder.RenameColumn(
                name: "total",
                table: "Orders",
                newName: "Total");

            migrationBuilder.RenameColumn(
                name: "ProductId",
                table: "OrderItems",
                newName: "ItemId");

            migrationBuilder.RenameIndex(
                name: "IX_OrderItems_ProductId",
                table: "OrderItems",
                newName: "IX_OrderItems_ItemId");

            migrationBuilder.RenameColumn(
                name: "ProductId",
                table: "CartItem",
                newName: "ItemId");

            migrationBuilder.RenameColumn(
                name: "ProductId",
                table: "BundleItem",
                newName: "ItemId");

            migrationBuilder.RenameIndex(
                name: "IX_BundleItem_ProductId",
                table: "BundleItem",
                newName: "IX_BundleItem_ItemId");

            migrationBuilder.RenameColumn(
                name: "ProductId",
                table: "Bundle",
                newName: "ItemId");

            migrationBuilder.RenameColumn(
                name: "Zipcode",
                table: "Address",
                newName: "ZipCode");

            migrationBuilder.AlterColumn<string>(
                name: "ItemId",
                table: "Reviews",
                type: "varchar(6)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "varchar(6)",
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Reviews",
                table: "Reviews",
                columns: new[] { "CartId", "ItemId" });

            migrationBuilder.AddForeignKey(
                name: "FK_BundleItem_Inventory_ItemId",
                table: "BundleItem",
                column: "ItemId",
                principalTable: "Inventory",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_OrderItems_Inventory_ItemId",
                table: "OrderItems",
                column: "ItemId",
                principalTable: "Inventory",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Reviews_Inventory_ItemId",
                table: "Reviews",
                column: "ItemId",
                principalTable: "Inventory",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BundleItem_Inventory_ItemId",
                table: "BundleItem");

            migrationBuilder.DropForeignKey(
                name: "FK_OrderItems_Inventory_ItemId",
                table: "OrderItems");

            migrationBuilder.DropForeignKey(
                name: "FK_Reviews_Inventory_ItemId",
                table: "Reviews");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Reviews",
                table: "Reviews");

            migrationBuilder.RenameColumn(
                name: "Total",
                table: "Orders",
                newName: "total");

            migrationBuilder.RenameColumn(
                name: "ItemId",
                table: "OrderItems",
                newName: "ProductId");

            migrationBuilder.RenameIndex(
                name: "IX_OrderItems_ItemId",
                table: "OrderItems",
                newName: "IX_OrderItems_ProductId");

            migrationBuilder.RenameColumn(
                name: "ItemId",
                table: "CartItem",
                newName: "ProductId");

            migrationBuilder.RenameColumn(
                name: "ItemId",
                table: "BundleItem",
                newName: "ProductId");

            migrationBuilder.RenameIndex(
                name: "IX_BundleItem_ItemId",
                table: "BundleItem",
                newName: "IX_BundleItem_ProductId");

            migrationBuilder.RenameColumn(
                name: "ItemId",
                table: "Bundle",
                newName: "ProductId");

            migrationBuilder.RenameColumn(
                name: "ZipCode",
                table: "Address",
                newName: "Zipcode");

            migrationBuilder.AlterColumn<string>(
                name: "ItemId",
                table: "Reviews",
                type: "varchar(6)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(6)");

            migrationBuilder.AddColumn<string>(
                name: "InventoryId",
                table: "Reviews",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Reviews",
                table: "Reviews",
                columns: new[] { "CartId", "InventoryId" });

            migrationBuilder.AddForeignKey(
                name: "FK_BundleItem_Inventory_ProductId",
                table: "BundleItem",
                column: "ProductId",
                principalTable: "Inventory",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_OrderItems_Inventory_ProductId",
                table: "OrderItems",
                column: "ProductId",
                principalTable: "Inventory",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Reviews_Inventory_ItemId",
                table: "Reviews",
                column: "ItemId",
                principalTable: "Inventory",
                principalColumn: "Id");
        }
    }
}
