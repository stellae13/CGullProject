using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CGullProject.Migrations
{
    /// <inheritdoc />
    public partial class AddCartStoredProcedures : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            string create_cart_details = $"CREATE OR ALTER PROC usp_GetCartDetails(@cartId int)" +
                $"AS " +
                $"SELECT CartItem.CartId, Product.Name, Product.Price AS 'UnitPrice', CartItem.Quantity, " +
                $"Product.Price * CartItem.Quantity AS 'Subtotal'FROM CartItem " +
                $"JOIN Product ON ProductId = Product.Id " +
                $"WHERE CartItem.CartId = @cartId;";

            string create_cart_totals = $"CREATE OR ALTER PROC usp_GetCartTotals(@cartId int) " +
                $"AS " +
                $"SELECT " +
                $"SUM (Product.Price * CartItem.Quantity) AS 'RegularTotal'" +
                $", SUM(Product.Price * CartItem.Quantity) AS 'BundleTotal'" +
                $", SUM(Product.Price * CartItem.Quantity) * 1.07 AS 'TotalWithTax' " +
                $"FROM CartItem " +
                $"JOIN Product ON ProductId = Product.Id " +
                $"WHERE CartItem.CartId = 1;";

            migrationBuilder.Sql(create_cart_details);
            migrationBuilder.Sql(create_cart_totals);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            string drop_cart_details = "drop proc usp_GetCartDetails;";
            string drop_cart_totals = "drop proc usp_GetCartTotals;";

            migrationBuilder.Sql(drop_cart_details);
            migrationBuilder.Sql(drop_cart_totals);
        }
    }
}
