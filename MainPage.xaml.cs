using PodB_MAUI.Model;
using System.Diagnostics;

namespace PodB_MAUI
{
    public partial class MainPage : ContentPage
    {
        private List<GroceryItem> Items;

        private void LoadItems()
        {
            //TODO: Load from Files, and Fake
            Items = FakeGroceryItems.DefaultItems.ToList<GroceryItem>();
        }

        public MainPage()
        {
            LoadItems();
            InitializeComponent();

            //Lets the UI see the item
            GroceryCollectionView.ItemsSource = Items;
        }

        // When Item is Pressed, Open the Details of it
        private async void OnItemTapped(object sender, TappedEventArgs e)
        {
            if (sender is BindableObject bindable && bindable.BindingContext is GroceryItem selectedItem)
            {
                await Shell.Current.GoToAsync(nameof(ItemDetailPage), new Dictionary<string, object>
            {
                { "SelectedItem", selectedItem }
            });
            }
        }

        // The Add Button -> Go to Add Item Scren
        private async void OnAddItemClicked(object sender, EventArgs e)
        {
            // TODO: Go To Add Item Screen
            await DisplayAlert("Add Item", "Navigate to add item modal/page here.", "OK");
        }
    }
}