namespace CGullProject
{
    public class Invetory
    {
        Dictionary<Item, int> Items {  get; set; }

        public Invetory() { 
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
            //TBD
        }
    }
}
