namespace PodB_MAUI.Model
{
    internal static class GroceryItems
    {
        public static List<GroceryItem> items = new List<GroceryItem>();
        static GroceryItems()
        {
            items.Add(
                new(name: "Apples",
                    category: GroceryCategory.PRODUCE,
                    price: 2.99,
                    quantityType: QuantityType.EACH,
                    description: "Fresh and crisp granny smith apples. Comes in red, green, and yellow varieties"
                )
            );

            items.Add(
                new(name: "Bananas",
                    category: GroceryCategory.PRODUCE,
                    price: 0.59,
                    quantityType: QuantityType.EACH,
                    description: "Fresh and ripe bananas. Comes in bunches of 5-7"
                )
            );

            items.Add(
                new(name: "Milk",
                    category: GroceryCategory.DAIRY,
                    price: 3.49,
                    quantityType: QuantityType.EACH,
                    description: "Comes in whole, 2%, 1%, and skim"
                )
            );

            items.Add(
                new(name: "Whole Wheat Bread",
                    category: GroceryCategory.BAKERY,
                    price: 2.50,
                    quantityType: QuantityType.EACH,
                    description: "Aunt May's Whole Wheat Bread, baked locally since 1996"
                )
            );

            items.Add(
                new(name: "95/5 Lean Ground Beef",
                    category: GroceryCategory.MEAT,
                    price: 8.70,
                    quantityType: QuantityType.POUND,
                    description: "Lean ground beef from Humble Farms, OH"
                )
            );

            items.Add(
                new(name: "Blueberry Muffin",
                    category: GroceryCategory.BAKERY,
                    price: 3.99,
                    quantityType: QuantityType.EACH,
                    description: "Blueberry muffins baked fresh daily"
                )
            );

        }

        public static bool exists(string name)
        {
            return items.Any(item => item.name.Equals(name, StringComparison.OrdinalIgnoreCase));
        }
    }
}