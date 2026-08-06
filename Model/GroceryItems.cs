namespace PodB_MAUI.Model
{
    internal static class GroceryItems
    {
        public static List<GroceryItem> items = new List<GroceryItem>();
        static GroceryItems()
        {
            items = [
                new(name: "Apples",
                    category: GroceryCategory.PRODUCE,
                    price: 2.99,
                    quantityType: QuantityType.EACH,
                    description: "Fresh and crisp granny smith apples. Comes in red, green, and yellow varieties",
                    photoUrl: "https://upload.wikimedia.org/wikipedia/commons/thumb/9/92/95apple.jpeg/250px-95apple.jpeg?utm_source=en.wikipedia.org&utm_campaign=parser&utm_content=thumbnail"
                ),
            
                new(name: "Milk",
                    category: GroceryCategory.DAIRY,
                    price: 3.49,
                    quantityType: QuantityType.EACH,
                    description: "Comes in whole, 2%, 1%, and skim",
                    photoUrl: "https://upload.wikimedia.org/wikipedia/commons/thumb/a/a5/Glass_of_Milk_%2833657535532%29.jpg/250px-Glass_of_Milk_%2833657535532%29.jpg"
                ),

                new(name: "Whole Wheat Bread",
                    category: GroceryCategory.BAKERY,
                    price: 2.50,
                    quantityType: QuantityType.EACH,
                    description: "Aunt May's Whole Wheat Bread, baked locally since 1996",
                    photoUrl: "https://upload.wikimedia.org/wikipedia/commons/thumb/c/c7/Korb_mit_Br%C3%B6tchen.JPG/250px-Korb_mit_Br%C3%B6tchen.JPG"
                ),

                new(name: "95/5 Lean Ground Beef",
                    category: GroceryCategory.MEAT,
                    price: 8.70,
                    quantityType: QuantityType.POUND,
                    description: "Lean ground beef from Humble Farms, OH"
                ),
            
                new(name: "Blueberry Muffin",
                    category: GroceryCategory.BAKERY,
                    price: 3.99,
                    quantityType: QuantityType.EACH,
                    description: "Blueberry muffins baked fresh daily"
                ),
            ];
        }

        public static List<GroceryItem> GetItems() { return  items; }

        public static bool exists(string name)
        {
            return items.Any(item => item.name.Equals(name, StringComparison.OrdinalIgnoreCase));
        }
    }
}