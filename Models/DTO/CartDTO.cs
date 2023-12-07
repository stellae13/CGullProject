namespace CGullProject.Models.DTO
{
    /// <summary>
    /// Used by GetCart endpoint, so that GetCart can tuple cart info (name & id)
    /// with an IEnumerable of CartItem entries. These entries contain named fields,
    /// provided by this struct when the frontend receives them in JS object
    /// literal notation.
    /// </summary>
    public class CartDTO
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = "";

        public IEnumerable<AbsCartItemView> Contents { get; set; } = new List<AbsCartItemView>();

        public abstract class AbsCartItemView
        {
            public AbsCartItemView(string id, int qty, decimal total)
            {
                ProductId = id;
                Quantity = qty;
                Total = total;
            }

            public string ProductId { get; set; }
            public int Quantity { get; set; }
            public bool IsBundle { get; set; }

            // Has to be present in base class, otherwise it never shows up in the
            // Json obj returned by GetCart, even if it's instantiated as a
            // BundleView subclass instance.
            public IEnumerable<string>? BundleItems { get; set; }
            public decimal Total { get; set; }
        }

        public class ProductView : AbsCartItemView
        {
            public ProductView(string id, int qty, decimal total) : base(id, qty, total)
            {
                IsBundle = false;
                BundleItems = null;

            }
        }

        public class BundleView : AbsCartItemView
        {
            public BundleView(string id, int qty, decimal total, IEnumerable<string> bundledProductIds) : base(id, qty, total)
            {
                BundledProductIds = bundledProductIds;
                IsBundle = true;

            }
            public IEnumerable<string>? BundledProductIds
            {
                get
                {
                    return BundleItems;
                }
                set
                {
                    BundleItems = value;
                }
            }
        }

    }
}
