using PodB_MAUI.Models;
using System.Collections.ObjectModel;
using System.Windows.Input;

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
                        MauiProgram.GetSelf().Name = value;
                    }
                }
            }
        }

        // Current list of peers
        public ObservableCollection<Peer> ActivePeers { get; set; } = new();

        private string _currentMessageText = string.Empty;
        public string CurrentMessageText
        {
            get => _currentMessageText;
            set
            {
                _currentMessageText = value;
                OnPropertyChanged(nameof(CurrentMessageText));
            }
        }

        // Keeps chat from all users and clients
        public ObservableCollection<Message> GlobalChatFeed { get; set; } = new();

        private Peer? _selectedPeer;
        public Peer? SelectedPeer
        {
            get => _selectedPeer;
            set
            {
                if (_selectedPeer != value)
                {
                    _selectedPeer = value;
                    OnPropertyChanged(nameof(SelectedPeer));
                    OnPropertyChanged(nameof(IsPeerSelected));
                }
            }
        }

        public bool IsPeerSelected => SelectedPeer != null;
        public ICommand SendCommand { get; }

        public MainPage()
        {
            //Init Subsystems
            MauiProgram.GetSelf();
            MauiProgram.GetNetwork();

            InitializeComponent();

            // Events
            MauiProgram.GetNetwork().OnPeersUpdated += OnNetworkPeersUpdated;
            MauiProgram.GetNetwork().OnMessageReceived += OnIncomingMessageReceived;
            MauiProgram.GetNetwork().Start();

            SendCommand = new Command(async () => await SendChatMessageAsync());
            this.BindingContext = this;
            Username = MauiProgram.GetSelf().Name;
        }

        private async Task SendChatMessageAsync()
        {
            if (SelectedPeer == null || string.IsNullOrWhiteSpace(CurrentMessageText))
                return;

            string messageToSend = CurrentMessageText.Trim();
            string targetIp = SelectedPeer.IpAddress;

            CurrentMessageText = string.Empty;

            // Transmit via TCP Client
            bool success = await MauiProgram.GetNetwork().SendMessageAsync(targetIp, messageToSend);

            if (success)
            {
                GlobalChatFeed.Add(new Message
                {
                    SenderName = Username,
                    Text = messageToSend,
                    Timestamp = DateTime.Now,
                    isOutgoing = true
                });
            }
            else
            {
                GlobalChatFeed.Add(new Message
                {
                    SenderName = "System Error",
                    Text = $"Failed to send message to {SelectedPeer.Name}.",
                    Timestamp = DateTime.Now,
                    isOutgoing = true
                });
            }
        }

        private void OnIncomingMessageReceived(Message message)
        {
            MainThread.BeginInvokeOnMainThread(() =>
            {
                GlobalChatFeed.Add(message);
            });
        }

        private void OnNetworkPeersUpdated(Peer[] updatedPeers)
        {
            MainThread.BeginInvokeOnMainThread(() =>
            {
                var incomingKeys = updatedPeers.Select(p => $"{p.IpAddress}:{p.Port}".ToLower()).ToHashSet();

                // 1. Clean up stale offline peers
                for (int i = ActivePeers.Count - 1; i >= 0; i--)
                {
                    var existing = ActivePeers[i];
                    string key = $"{existing.IpAddress}:{existing.Port}".ToLower();
                    if (!incomingKeys.Contains(key))
                    {
                        if (SelectedPeer == existing)
                        {
                            SelectedPeer = null;
                        }
                        ActivePeers.RemoveAt(i);
                    }
                }

                // 2. Add new discoveries or update changed names in place
                foreach (var incoming in updatedPeers)
                {
                    string key = $"{incoming.IpAddress}:{incoming.Port}".ToLower();
                    var existing = ActivePeers.FirstOrDefault(p => $"{p.IpAddress}:{p.Port}".ToLower() == key);

                    if (existing != null)
                    {
                        if (existing.Name != incoming.Name)
                        {
                            existing.Name = incoming.Name;
                        }
                        existing.LastSeen = incoming.LastSeen;
                    }
                    else
                    {
                        ActivePeers.Add(new Peer
                        {
                            Name = incoming.Name,
                            IpAddress = incoming.IpAddress,
                            Port = incoming.Port,
                            LastSeen = incoming.LastSeen
                        });
                    }
                }
            });
        }
    }
}