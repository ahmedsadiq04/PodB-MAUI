using FFImageLoading;
using Microsoft.Extensions.DependencyInjection;
using FFImageLoading;
using FFImageLoading.Config;

namespace PodB_MAUI
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();
        }

        //Happens when app is done loading (after starting)
        protected override void OnStart()
        {
            base.OnStart();

            // Create a HTTP Client to force images to load
            var httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.Add("User-Agent", "Mozilla/5.0 (Windows NT 10.0; Win64; x64)");

            ImageService.Instance.Initialize(new Configuration
            {
                HttpClient = httpClient,
                HttpHeadersTimeout = 15
            });
        }

        protected override Window CreateWindow(IActivationState? activationState)
        {
            return new Window(new AppShell());
        }
    }
}