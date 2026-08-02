using PodB_MAUI.Model;
using System.Xml;

namespace PodB_MAUI
{
    [QueryProperty(nameof(Item), "SelectedItem")]
    public partial class ItemDetailPage : ContentPage
    {
        public GroceryItem Item
        {
            set
            {
                NameLabel.Text = value.name;
                CategoryLabel.Text = $"Category: {value.category}";
                PriceLabel.Text = $"Price: {value.price:C}";

                if (StatusAssets.ImagePaths.TryGetValue(value.category, out var imageUrl))
                {
                    CategoryImage.Source = imageUrl;
                }
            }
        }

        public ItemDetailPage()
        {
            InitializeComponent();
        }
    }
}