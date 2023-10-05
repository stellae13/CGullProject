namespace CGullProject
{
    public class ShoppingCart
    {
        Dictionary<Item, int> Cart { get; set; }

        Inventory Inventory { get; set; }

        public ShoppingCart(Inventory inventory) { 
            Cart = new Dictionary<Item, int>();
            Inventory = inventory;
        }

        public bool AddToCart(Item item, int quantity)
        {
            if (Inventory.Items[item] >= quantity) {

                Cart.Add(item, quantity);
                Inventory.EditQuantity(item, Inventory.Items[item] - quantity);
                return true;
            }

            return false;
        }
    }
}
