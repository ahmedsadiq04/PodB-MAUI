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

        private void OnNameChanged(object sender, TextChangedEventArgs e)
        {
            
        }

        private void OnDescriptionChanged(object sender, EventArgs e)
        {
            
        }
    }
}