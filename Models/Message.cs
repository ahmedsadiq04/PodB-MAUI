using System;
using System.Collections.Generic;
using System.Text;

namespace PodB_MAUI.Models
{
    public class Message
    {
        public string SenderName { get; set; } = string.Empty;
        public string Text { get; set; } = string.Empty;
        public DateTime Timestamp { get; set; }
    }
}
