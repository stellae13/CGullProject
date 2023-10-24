using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CGullProject.Migrations
{
    /// <inheritdoc />
    public partial class CategoryIdChange : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "CategoryId",
                table: "Product",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(64)");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "CategoryId",
                table: "Product",
                type: "varchar(64)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");
        }
    }
}
