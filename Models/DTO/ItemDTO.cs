namespace CGullProject.Models.DTO {
    /// <summary>
    /// Data transfer object for the Item entity
    /// </summary>
    public class ItemDTO
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
