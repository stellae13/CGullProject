namespace CGullProject.Models.DTO {
    /// <summary>
    /// DAta transfer object for the Product entity
    /// </summary>
    public class ProductDTO
    {
        public string Id { get; set; } = string.Empty;

        public string Name { get; set; } = string.Empty;

        public int CategoryId { get; set; }

        public decimal MSRP { get; set; }

        public decimal SalePrice { get; set; }

        public decimal Rating { get; set; }

        public int Stock { get; set; }

        public bool IsBundle {  get; set; }

    }
}
