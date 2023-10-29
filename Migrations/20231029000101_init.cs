using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CGullProject.Migrations
{
    /// <inheritdoc />
    public partial class init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Cart",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier default NEWID()", nullable: false),
                    Name = table.Column<string>(type: "varchar(64)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cart", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Category",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "varchar(32)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Category", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CartItem",
                columns: table => new
                {
                    CartId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ProductId = table.Column<string>(type: "varchar(6)", nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CartItem", x => new { x.CartId, x.ProductId });
                    table.ForeignKey(
                        name: "FK_CartItem_Cart_CartId",
                        column: x => x.CartId,
                        principalTable: "Cart",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Inventory",
                columns: table => new
                {
                    Id = table.Column<string>(type: "varchar(6)", nullable: false),
                    Name = table.Column<string>(type: "varchar(32)", nullable: false),
                    CategoryId = table.Column<int>(type: "int", nullable: false),
                    MSRP = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    SalePrice = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Rating = table.Column<decimal>(type: "decimal(3,2)", nullable: false),
                    Stock = table.Column<int>(type: "int", nullable: false),
                    isBundle = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Inventory", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Inventory_Category_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Category",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Bundle",
                columns: table => new
                {
                    Id = table.Column<string>(type: "varchar(6)", nullable: false),
                    ProductId = table.Column<string>(type: "varchar(6)", nullable: true),
                    StartDate = table.Column<DateTime>(type: "datetime", nullable: false),
                    EndDate = table.Column<DateTime>(type: "datetime", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Bundle", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Bundle_Inventory_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Inventory",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "BundleItem",
                columns: table => new
                {
                    BundleId = table.Column<string>(type: "varchar(6)", nullable: false),
                    ProductId = table.Column<string>(type: "varchar(6)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BundleItem", x => new { x.BundleId, x.ProductId });
                    table.ForeignKey(
                        name: "FK_BundleItem_Bundle_BundleId",
                        column: x => x.BundleId,
                        principalTable: "Bundle",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BundleItem_Inventory_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Inventory",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Bundle_ProductId",
                table: "Bundle",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_BundleItem_ProductId",
                table: "BundleItem",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_Inventory_CategoryId",
                table: "Inventory",
                column: "CategoryId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BundleItem");

            migrationBuilder.DropTable(
                name: "CartItem");

            migrationBuilder.DropTable(
                name: "Bundle");

            migrationBuilder.DropTable(
                name: "Cart");

            migrationBuilder.DropTable(
                name: "Inventory");

            migrationBuilder.DropTable(
                name: "Category");
        }
    }
}
