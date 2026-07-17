using PodB_MAUI.Models;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Text.Json;

namespace PodB_MAUI.Network
{
    public class NetworkSubsystem
    {
        private static readonly string AppInstanceId = Guid.NewGuid().ToString(); //for testing TODO: destroy

        private const int DiscoveryPort = 50001; // used to constantly ping and say - here we are
        private const int MessagePort   = 50002; // used to send messages over this port
        private const int SleepDurationPerBroadcast = 5000; //5s (in ms)

        private UdpClient? udpListener;
        private CancellationTokenSource? cts;
        private bool bIsRunning;

        private readonly Dictionary<string, Peer> _peers = new();
        private readonly object _peerLock = new();


        //Returns the list of Peers on the same network
        public Peer[] GetActivePeers()
        {
            lock (_peerLock)
            {
                return _peers.Values.ToArray();
            }
        }

        //Sends event once peer is updated
        public event Action<Peer[]>? OnPeersUpdated;

        public void Start()
        {
            if(bIsRunning) return;
            bIsRunning = true;
            cts = new CancellationTokenSource();

            Task.Run(() => StartListeningAsync(cts.Token));
            Task.Run(() => StartBroadcastingAsync(cts.Token));
        }

        public void Stop()
        {
            bIsRunning = false;
            cts?.Cancel();
            udpListener?.Close();
        }

        private async Task StartBroadcastingAsync(CancellationToken token)
        {
            using var sender = new UdpClient();
            sender.EnableBroadcast = true;

            var endpoint = new IPEndPoint(IPAddress.Broadcast, DiscoveryPort);

            while (!token.IsCancellationRequested)
            {
                try
                {
                    string currentName = MauiProgram.GetSelf().Name ?? "Unknown Peer";
                    var payload = new DiscoveryPacket
                    {
                        Name = currentName,
                        TcpPort = MessagePort,
                        AppID = AppInstanceId,
                    };

                    string json = JsonSerializer.Serialize(payload);
                    byte[] bytes = Encoding.UTF8.GetBytes(json);

                    await sender.SendAsync(bytes, bytes.Length, endpoint);
                }
                catch (Exception ex)
                {
                    System.Diagnostics.Debug.WriteLine($"Broadcast Error: {ex.Message}");
                }

                // Sleep to stop spamming per second
                await Task.Delay(SleepDurationPerBroadcast, token);
            }
        }

        private async Task StartListeningAsync(CancellationToken token)
        {
            try
            {
                udpListener = new UdpClient();
                udpListener.Client.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.ReuseAddress, true);
                udpListener.Client.Bind(new IPEndPoint(IPAddress.Any, DiscoveryPort));
                udpListener.EnableBroadcast = true;

                while (!token.IsCancellationRequested)
                {
                    var result = await udpListener.ReceiveAsync(token);

                    // Ignore self  - disabled during test using UUID
                    /*
                    if (IsSelfAddress(result.RemoteEndPoint.Address))
                        continue;
                    */

                    string json = Encoding.UTF8.GetString(result.Buffer);
                    var payload = JsonSerializer.Deserialize<DiscoveryPacket>(json);

                    if (payload != null)
                    {
                        if (payload.AppID == AppInstanceId)
                            continue;

                        string senderIp = result.RemoteEndPoint.Address.ToString();
                        int senderPort = payload.TcpPort;
                        string uniqueKey = $"{senderIp}:{senderPort}".ToLower();

                        lock (_peerLock)
                        {
                            if (_peers.TryGetValue(uniqueKey, out var existingPeer))
                            {
                                existingPeer.Name = payload.Name;
                                existingPeer.LastSeen = DateTime.Now;
                            }
                            else
                            {
                                var newPeer = new Peer
                                {
                                    Name = payload.Name,
                                    IpAddress = senderIp,
                                    Port = senderPort,
                                    LastSeen = DateTime.Now
                                };
                                _peers[uniqueKey] = newPeer;
                            }
                        }

                        // Fire event for the UI
                        var currentSnapshot = GetActivePeers();
                        MainThread.BeginInvokeOnMainThread(() => { OnPeersUpdated?.Invoke(currentSnapshot); });
                    }
                }
            }
            catch (OperationCanceledException)
            {
                // Normal Exit
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Listener Error: {ex.Message}");
            }
        }

        private bool IsSelfAddress(IPAddress address)
        {
            var host = Dns.GetHostEntry(Dns.GetHostName());
            foreach (var ip in host.AddressList)
            {
                if (ip.Equals(address)) return true;
            }
            return false;
        }

        public class DiscoveryPacket
        {
            public string AppID { get; set; } = string.Empty;
            public string Name { get; set; } = string.Empty;
            public int TcpPort { get; set; }
        }
    }
}
