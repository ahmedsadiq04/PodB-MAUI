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
        private TcpListener? tcpListener;

        private CancellationTokenSource? cts;
        private bool bIsRunning;

        private readonly Dictionary<string, Peer> _peers = new();
        private readonly object _peerLock = new();

        private static readonly JsonSerializerOptions JsonOptions = new()
        {
            PropertyNameCaseInsensitive = true
        };


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
        public event Action<Message>? OnMessageReceived;

        public void Start()
        {
            if(bIsRunning) return;
            bIsRunning = true;
            cts = new CancellationTokenSource();

            Task.Run(() => StartListeningAsync(cts.Token));
            Task.Run(() => StartBroadcastingAsync(cts.Token));
            Task.Run(() => StartTcpListenerAsync(cts.Token));
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

        public async Task<bool> SendMessageAsync(string targetIp, string text)
        {
            try
            {
                using var client = new TcpClient();
                var connectTask = client.ConnectAsync(targetIp, MessagePort);
                var delayTask = Task.Delay(3000);

                var completedTask = await Task.WhenAny(connectTask, delayTask);
                if (completedTask == delayTask)
                {
                    System.Diagnostics.Debug.WriteLine($"[TCP Send] Connection timed out to {targetIp}");
                    return false;
                }

                var packet = new MessagePacket
                {
                    SenderName = MauiProgram.GetSelf().Name,
                    MessageText = text,
                    Timestamp = DateTime.Now,
                    AppID = AppInstanceId
                };

                string json = JsonSerializer.Serialize(packet);
                byte[] bytes = Encoding.UTF8.GetBytes(json);

                using var stream = client.GetStream();
                await stream.WriteAsync(bytes, 0, bytes.Length);
                await stream.FlushAsync();

                return true;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"[TCP Send Error] {ex.Message}");
                return false;
            }
        }

        private async Task StartTcpListenerAsync(CancellationToken token)
        {
            try
            {
                tcpListener = new TcpListener(IPAddress.Any, MessagePort);
                tcpListener.Start();

                while (!token.IsCancellationRequested)
                {
                    var client = await tcpListener.AcceptTcpClientAsync(token);
                    _ = Task.Run(() => ProcessIncomingConnectionAsync(client), token);
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"[TCP Server Error] {ex.Message}");
            }
        }

        private async Task ProcessIncomingConnectionAsync(TcpClient client)
        {
            using (client)
            using (var stream = client.GetStream())
            using (var ms = new MemoryStream())
            {
                try
                {
                    byte[] buffer = new byte[2048];
                    int bytesRead;

                    while ((bytesRead = await stream.ReadAsync(buffer, 0, buffer.Length)) > 0)
                    {
                        ms.Write(buffer, 0, bytesRead);
                    }

                    string json = Encoding.UTF8.GetString(ms.ToArray());
                    var packet = JsonSerializer.Deserialize<MessagePacket>(json);

                    if (packet != null && packet.AppID != AppInstanceId)
                    {
                        var chatMsg = new Message
                        {
                            SenderName = packet.SenderName,
                            Text = packet.MessageText,
                            Timestamp = packet.Timestamp,
                            isOutgoing = false
                        };

                        OnMessageReceived?.Invoke(chatMsg);
                    }
                }
                catch (Exception ex)
                {
                    System.Diagnostics.Debug.WriteLine($"[Process Inbound Msg Error] {ex.Message}");
                }
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
