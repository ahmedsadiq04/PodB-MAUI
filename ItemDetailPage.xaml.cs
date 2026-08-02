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
                NameLabel.Text = value.Name;
                CategoryLabel.Text = $"Category: {value.Category}";
                PriceLabel.Text = $"Price: {value.Price:C}";

                if (StatusAssets.ImagePaths.TryGetValue(value.Category, out var imageUrl))
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