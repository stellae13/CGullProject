using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CGullProject.Migrations
{
    /// <inheritdoc />
    public partial class reviews2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<decimal>(
                name: "Rating",
                table: "Inventory",
                type: "decimal(3,2)",
                nullable: false,
                computedColumnSql: "select avg(Inventory.rating) from Reviews, Inventory where Inventory.Id = Reviews.InventoryId",
                oldClrType: typeof(decimal),
                oldType: "decimal(3,2)");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<decimal>(
                name: "Rating",
                table: "Inventory",
                type: "decimal(3,2)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(3,2)",
                oldComputedColumnSql: "select avg(Inventory.rating) from Reviews, Inventory where Inventory.Id = Reviews.InventoryId");
        }
    }
}
