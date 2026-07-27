namespace PodB_MAUI
{
    public partial class MainPage : ContentPage
    {
        int count = 0;

        public MainPage()
        {
            InitializeComponent();
        }

        private void OnCounterClicked(object? sender, EventArgs e)
        {
            count++;

            if (count == 1)
                CounterBtn.Text = $"Clicked {count} time";
            else
                CounterBtn.Text = $"Clicked {count} times";

            SemanticScreenReader.Announce(CounterBtn.Text);
        }

        private async void OnTestDetailClicked(object sender, EventArgs e)
        {
            var testItem = new PodB_MAUI.Model.GroceryItem("Test Apple", PodB_MAUI.Model.GroceryCategory.PRODUCE, 1.29);
            await Shell.Current.GoToAsync(nameof(ItemDetailPage), new Dictionary<string, object>
            {
                { "SelectedItem", testItem }
            });
        }
    }
}