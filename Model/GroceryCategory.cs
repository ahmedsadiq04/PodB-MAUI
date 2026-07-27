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
            //TODO: Replace the *** with the actual image file names for each category. Do we want to use pngs or xmls?
        { GroceryCategory.PRODUCE, "Resources/Images/***.png" },
        { GroceryCategory.DAIRY, "Resources/Images/***.png" },
        { GroceryCategory.MEAT, "Resources/Images/***.png" },
        { GroceryCategory.BAKERY, "Resources/Images/***.png" }
    };
    }
}
