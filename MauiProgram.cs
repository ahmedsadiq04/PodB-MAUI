using Microsoft.Extensions.Logging;
using PodB_MAUI.Models;
using PodB_MAUI.Network;

namespace PodB_MAUI
{
    public static class MauiProgram
    {
        //Singleton for self connection
        private static Peer selfConnection;
        private static NetworkSubsystem network;
        public static Peer GetSelf()
        {
            if(selfConnection == null)
            {
                selfConnection = new Peer();
                int randomCode = Random.Shared.Next(1000, 10000);
                selfConnection.Name = $"User-{randomCode}";
            }

            return selfConnection;
        }

        public static NetworkSubsystem GetNetwork()
        {
            if (network == null) network = new NetworkSubsystem();
            return network;
        }

        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                });

#if DEBUG
    		builder.Logging.AddDebug();
#endif

            return builder.Build();
        }
    }
}
