using System.Runtime.CompilerServices;

namespace CGullProject.Models
{
    // Used by GetCart endpoint, so that GetCart can Tuple together
    // the cart info (name & id) with an IEnumerable of CartItem entries,
    // with named fields, provided by this struct, when FE receives them
    // in js object literal notation.
    public class CartDTO
    {
        public Guid Id { get; set; }
        public String Name { get; set; }

        public IEnumerable<AbsCartItemView> Contents { get; set; }






        public abstract class AbsCartItemView
        {
            public AbsCartItemView(string id, int qty, decimal total)
            {
                ProductId = id;
                Quantity = qty;
                Total = total;
            }
            
            public String ProductId { get; set; }
            public int Quantity { get; set; }
            public bool IsBundle { get; set; }

            // Has to be present in base class, otherwise it never shows up in the
            // Json obj returned by GetCart, even if it's instantiated as a
            // BundleView subclass instance.
            public IEnumerable<String>? BundleItems { get; set; }
            public decimal Total { get; set; }
        }

        public class ProductView : AbsCartItemView
        {
            public ProductView(String id, int qty, decimal total) : base(id, qty, total) {
                base.IsBundle = false;
                base.BundleItems = null;
                
            }
        }

        public class BundleView : AbsCartItemView
        {
            public BundleView(String id, int qty, decimal total, IEnumerable<String> bundledProductIds) : base(id, qty, total)
            {
                BundledProductIds = bundledProductIds;
                base.IsBundle = true;

            }
            public IEnumerable<String>? BundledProductIds { 
                get {
                    return base.BundleItems;
                }
                set
                {
                    base.BundleItems = value;
                }
            }
        }

    }
}
