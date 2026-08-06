namespace PodB_MAUI
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();
            Routing.RegisterRoute(nameof(ItemList), typeof(ItemList));
            Routing.RegisterRoute(nameof(AddItemScreen), typeof(AddItemScreen));
            Routing.RegisterRoute(nameof(ItemDetailPage), typeof(ItemDetailPage));
            //TODO: Maybe needs a router per page
        }
    }
}