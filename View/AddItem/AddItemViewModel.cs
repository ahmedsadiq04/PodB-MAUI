using CommunityToolkit.Maui.Alerts;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using PodB_MAUI.Model;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace PodB_MAUI.View.AddItem
{
    public partial class AddItemViewModel : ObservableObject
    {
        [ObservableProperty]
        private string productName = string.Empty;

        [ObservableProperty]
        private string description = string.Empty;

        [ObservableProperty]
        private GroceryCategory selectedCategory;

        [ObservableProperty]
        private QuantityType selectedQuantity;

        [ObservableProperty]
        private string price;

        [ObservableProperty]
        private string photoUrl = string.Empty;



        [RelayCommand]
        private async Task Submit()
        {
            double priceDec;

            //No name given
            if (string.IsNullOrWhiteSpace(ProductName))
            {
                await Toast.Make(Resources.Strings.err_empty_name).Show();
                return;
            }

            //Duplicate name
            else if (GroceryItems.exists(ProductName))
            {
                await Toast.Make(String.Format(Resources.Strings.err_duplicate_name, productName)).Show();
                return;
            }

            //Price not parsable
            else if (!double.TryParse(price, out priceDec))
            {
                await Toast.Make(String.Format(Resources.Strings.err_price_parse, price)).Show();
                return;
            }

            await Toast.Make(String.Format(Resources.Strings.succ_add_item, productName)).Show();

            GroceryItem item = new GroceryItem(
                name: productName,
                category: selectedCategory,
                price: priceDec,
                quantityType: selectedQuantity,
                description: description
            );
            GroceryItems.items.Add(item);

            return;
        }

        [RelayCommand]
        private async Task Close()
        {
            await Shell.Current.GoToAsync("..");
        }
        
        public ObservableCollection<GroceryCategory> Categories { get; } =
            new(Enum.GetValues<GroceryCategory>());

        public ObservableCollection<QuantityType> Quantities { get; } =
            new(Enum.GetValues<QuantityType>());
    }
}
