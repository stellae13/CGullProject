namespace CGullProject
{
    public class Item
    {
        public decimal Price { get; set; }
        public string Name { get; set; }    
        public Guid ItemID { get; set; }
        public Category Cat { get; set; }

        public Item() {
            Price = 0;
            Name = "";
            ItemID = Guid.NewGuid();
            Cat = new Category();
        }

        public Item(decimal price, string name, Category cat) {
            Price = price;
            Name = name;
            ItemID = Guid.NewGuid();
            Cat = cat;
        }
    
    }

    public enum Category
    {
        GROCERIES = 1,
        ELECTRONICS = 2,
        CLOTHING = 3,
        HOME_AND_FURNITURE = 4,
        BEAUTY_AND_PERSONAL_CARE = 5,
        SPORTS_AND_OUTDOORS = 6,
        AUTOMOTIVE = 7,
        TOYS_AND_GAMES = 8,
        BOOKS_AND_MEDIA = 9,
        OFFICE_SUPPLIES = 10,
        PHARMACY_AND_HEALTH = 11,
        PET_SUPPLIES = 12,
        JEWELRY_AND_ACCESSORIES = 13,
        BABY_AND_TODDLER = 14
    }
}
