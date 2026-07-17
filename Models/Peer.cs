using System;
using System.Collections.Generic;
using System.Text;

namespace PodB_MAUI.Models
{
    public class Peer
    {
        public string Name { get; set; } = string.Empty;
        public string IpAddress { get; set; } = string.Empty;
        public int Port { get; set; }
        public DateTime LastSeen { get; set; }
    }
}
