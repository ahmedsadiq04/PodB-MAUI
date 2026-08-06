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
            { GroceryCategory.PRODUCE, "Resources/Images/produce.png" },
            { GroceryCategory.DAIRY, "Resources/Images/dairy.png" },
            { GroceryCategory.MEAT, "Resources/Images/meat.png" },
            { GroceryCategory.BAKERY, "Resources/Images/bakery.png" }
        };
    }
}
