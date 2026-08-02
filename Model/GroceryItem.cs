using System;
using System.Collections.Generic;
using System.Text;


namespace PodB_MAUI.Model
{
    public record GroceryItem
    {
        public GroceryItem(string name, GroceryCategory category, double price)
        {
            Name = name;
            Category = category;
            Price = price;
        }

        public GroceryItem(string name, GroceryCategory category, double price, string unit)
        {
            Name = name;
            Category = category;
            Price = price;
            Unit = unit;
        }

        public string Name { get; init; } = string.Empty;
        public string Image { get; init; } = string.Empty; //Url for the image
        public GroceryCategory Category { get; init; }

        public double Price { get; init; }
        public int Quantity { get; init; }
        public string Unit { get; init; } = "Unit"; //$price x quantity $(Unit)

        public double TotalPrice => Price * Quantity;

        public static readonly GroceryItem[] DefaultItems =
        [
            new("Apples", GroceryCategory.PRODUCE, 2.99),
            new("Milk", GroceryCategory.DAIRY, 3.49, "Gallon"),
            new("Whole Wheat Bread", GroceryCategory.BAKERY, 2.50),
            new("95/5 Lean Ground Beef", GroceryCategory.MEAT, 8.70, "lb"),
            new("Blueberry Muffin", GroceryCategory.BAKERY, 3.99),
        ];
    }
}

// name
// category
// price
// description
// image_url -> https://path_to_image.jpg
