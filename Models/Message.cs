using System;
using System.Collections.Generic;
using System.Text;

namespace PodB_MAUI.Models
{
    public class Message
    {
        public bool isOutgoing { get; set; } = false;
        public string SenderName { get; set; } = string.Empty;
        public string Text { get; set; } = string.Empty;
        public DateTime Timestamp { get; set; }
    }

    public class MessagePacket
    {
        public string SenderName { get; set; } = string.Empty;
        public string MessageText { get; set; } = string.Empty;
        public DateTime Timestamp { get; set; }
        public string AppID { get; set; } = string.Empty;
    }
}
