namespace CGullProject.Models.DTO
{
    public class TotalsDTO
    {
        public static readonly decimal FederalSalesTaxRate = .06M;
        public decimal RegularTotal {  get; set; }
        public decimal BundleTotal { get; set; }

        public decimal TotalWithTax { get; set; }
    }
}
