using System;
using System.Collections.Generic;
using System.Text;

namespace PodB_MAUI.Model
{
    public enum GroceryCategory
    {
        PRODUCE,
        DAIRY,
        MEAT,
        BAKERY
    }

    public static class StatusAssets
    {
        public static readonly Dictionary<GroceryCategory, string> ImagePaths = new()
        {
            { GroceryCategory.PRODUCE, "https://cdn-icons-png.flaticon.com/512/2909/2909808.png" },
            { GroceryCategory.DAIRY, "https://cdn-icons-png.flaticon.com/512/2674/2674486.png" },
            { GroceryCategory.MEAT, "https://cdn-icons-png.flaticon.com/512/1046/1046751.png" },
            { GroceryCategory.BAKERY, "https://cdn-icons-png.flaticon.com/512/3014/3014520.png" },
        };
    }
}
