namespace CGullProject
{
    public class Inventory
    {
        public Dictionary<Item, int> Items {  get; set; }

        public Inventory() { 
            Items = new Dictionary<Item, int>();
        }

        public void AddItem(Item item, int quantity) {

            Items.Add(item, quantity);
        }
        public void RemoveItem(Item item)
        {
            Items.Remove(item);
        }

        public void EditQuantity(Item item, int newQuantity)
        {
            Items[item] = newQuantity;
        }
    }
}
