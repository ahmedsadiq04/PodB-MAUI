using PodB_MAUI.Models;
using System.Collections.ObjectModel;

namespace PodB_MAUI
{
    public partial class MainPage : ContentPage
    {
        private string _Username = "UNKNOWN";
        public string Username
        {
            get => _Username;
            set
            {
                if (_Username != value)
                {
                    _Username = value;
                    OnPropertyChanged(nameof(Username));

                    if (MauiProgram.GetSelf() != null)
                    {
                        MauiProgram.GetSelf().Name = value; //Updates the username
                    }
                }
            }
        }

        //Peer ref for the UI
        public ObservableCollection<Peer> ActivePeers { get; set; } = new();

        private void OnNetworkPeersUpdated(Peer[] updatedPeers)
        {
            //update on the main thread
            MainThread.BeginInvokeOnMainThread(() =>
            {
                ActivePeers.Clear();
                foreach (var peer in updatedPeers)
                {
                    ActivePeers.Add(new Peer
                    {
                        Name = peer.Name,
                        IpAddress = peer.IpAddress,
                        Port = peer.Port,
                        LastSeen = peer.LastSeen
                    });
                }
            });
        }

        public MainPage()
        {
            //Start Network Subsystem
            MauiProgram.GetNetwork().OnPeersUpdated += OnNetworkPeersUpdated; //Bind the event
            MauiProgram.GetNetwork().Start();

            InitializeComponent();
            this.BindingContext = this;
            Username = MauiProgram.GetSelf().Name;
        }
    }
}
