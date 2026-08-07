namespace PodB_MAUI.Model
{
    public record GroceryItem(
        string name,
        GroceryCategory category,
        double price,
        string description = "No description available",
        string photoUrl = "https://upload.wikimedia.org/wikipedia/commons/a/a3/Image-not-found.png",
        QuantityType quantityType = QuantityType.UNIT
    );
}
