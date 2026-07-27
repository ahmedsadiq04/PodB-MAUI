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
        { GroceryCategory.PRODUCE, "icons/produce.png" },
        { GroceryCategory.DAIRY, "icons/alert.png" },
        { GroceryCategory.MEAT, "icons/cross.png" },
        { GroceryCategory.BAKERY, "icons/check.png" }
    };
    }
}
