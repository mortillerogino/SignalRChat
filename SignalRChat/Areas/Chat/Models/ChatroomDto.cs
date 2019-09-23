using SignalRChat.Areas.Identity.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SignalRChat.Areas.Chat.Models
{
    public class ChatroomDto
    {
        public int Id { get; }
        public string Name { get; }
        public ICollection<ChatMessage> ChatMessages { get; }
        public ChatUser User { get; private set; }

        public ChatroomDto(Chatroom chatroom)
        {
            this.Id = chatroom.Id;
            this.Name = chatroom.Name;
            this.ChatMessages = chatroom.ChatMessages;
        }

        public void SetUser(ChatUser user)
        {
            this.User = user;
        }
    }
}
