using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using PodB_MAUI.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace PodB_MAUI.View.AddItem
{
    public partial class AddItemViewModel : ObservableObject
    {
        public ObservableCollection<GroceryCategory> Categories { get; } =
            new(Enum.GetValues<GroceryCategory>());

        [ObservableProperty]
        private string productName = string.Empty;

        [ObservableProperty]
        private string description = string.Empty;

        [ObservableProperty]
        private GroceryCategory selectedCategory;

        [ObservableProperty]
        private string photoUrl = string.Empty;



        [RelayCommand]
        private void Save() { }
    }
}
