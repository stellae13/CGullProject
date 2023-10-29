namespace CGullProject.Models
{
    // Used by GetCart endpoint, so that GetCart can Tuple together
    // the cart info (name & id) with an IEnumerable of CartItem entries,
    // with named fields, provided by this struct, when FE receives them
    // in js object literal notation.
    public struct CartItemView
    {
        public String InventoryId { get; set; }
        public int Quantity { get; set; }
        public Decimal Total { get; set; }

    }
}
