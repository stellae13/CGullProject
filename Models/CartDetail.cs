namespace CGullProject.Models
{
    public class CartDetail
    {
        public int CartId { get; set; }
        public string Name { get; set; } = string.Empty;
        public decimal UnitPrice { get; set; }
        public int Quantity { get; set; }
        public decimal Subtotal { get; set; }
    }
}
