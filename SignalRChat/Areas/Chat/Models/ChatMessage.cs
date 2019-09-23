using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SignalRChat.Areas.Chat.Models
{
    public class ChatMessage
    {
        public int Id { get; set; }
        public string Message { get; set; }
        public string Username { get; set; }
        public int ChatroomId { get; set; }
        public Chatroom Chatroom { get; set; }
    }
}
