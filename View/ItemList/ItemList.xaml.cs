using PodB_MAUI.Model;
using System.Diagnostics;

namespace PodB_MAUI
{
    public partial class ItemList : ContentPage
    {
        private List<GroceryItem> Items;

        private void LoadItems()
        {
            //TODO: Load from Files, and Fake
            Items = GroceryItems.GetItems();
        }

        public ItemList()
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
            await Shell.Current.GoToAsync(nameof(AddItemScreen));
        }
    }
}