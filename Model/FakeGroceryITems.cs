namespace PodB_MAUI.Model
{
    internal class FakeGroceryItems
    {
        public static readonly GroceryItem[] DefaultItems =
        [
            new( name:"Apples",
                category:GroceryCategory.PRODUCE,
                price: 2.99,
                quantityType: QuantityType.EACH,
                description: "Fresh and crisp granny smith apples. Comes in red, green, and yellow varieties"
                ),
            
            new(name:"Milk",
                category: GroceryCategory.DAIRY,
                price: 3.49,
                quantityType: QuantityType.EACH,
                description: "Comes in whole, 2%, 1%, and skim"
                ),

            new(name:"Whole Wheat Bread",
                category:  GroceryCategory.BAKERY,
                price: 2.50,
                quantityType: QuantityType.EACH,
                description: "Aunt May's Whole Wheat Bread, baked locally since 1996"
                ),

            new(name:"95/5 Lean Ground Beef",
                category: GroceryCategory.MEAT,
                price: 8.70,
                quantityType: QuantityType.POUND,
                description: "Lean ground beef from Humble Farms, OH"
                ),
            
            new(name:"Blueberry Muffin",
                category: GroceryCategory.BAKERY,
                price: 3.99,
                quantityType: QuantityType.EACH,
                description: "Blueberry muffins baked fresh daily"
                ),
        ];
    }
}
