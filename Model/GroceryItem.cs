using System;
using System.Collections.Generic;
using System.Text;


namespace PodB_MAUI.Model
{
    public record GroceryItem {
        public string Name { get; init; } = string.Empty;
        public string Image { get; init; } = string.Empty; //Url for the image
        public GroceryCategory Category { get; init; }

        public double Price { get; init; }
        public int Quantity { get; init; }
        public string Unit { get; init; } = "Unit(s)"; //$price x quantity $(Unit)

        public double TotalPrice => Price * Quantity
    }
}


// name
// category
// price
// description
// image_url -> https://path_to_image.jpg
