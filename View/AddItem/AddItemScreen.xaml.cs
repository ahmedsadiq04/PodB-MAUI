
using CommunityToolkit.Maui.Alerts;
using CommunityToolkit.Maui.Core;
using PodB_MAUI.View.AddItem;

namespace PodB_MAUI
{
	public partial class AddItemScreen : ContentPage
	{
		public AddItemScreen()
		{
            InitializeComponent();
            BindingContext = new AddItemViewModel();
        }
    }
}