using SignalRChat.Areas.Identity.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SignalRChat.Areas.Chat.Models
{
    public class ChatroomDto
    {
        public int Id { get; }
        public string Name { get; }

        private ICollection<ChatMessage> _chatMessages;
        public ICollection<ChatMessage> ChatMessages
        {
            get
            {
                if (_chatMessages == null)
                {
                    _chatMessages = new List<ChatMessage>();
                }

                return _chatMessages;
            }
        }

        private ICollection<ChatUser> _chatUsers;

        [DisplayName("Members")]
        public ICollection<ChatUser> ChatUsers
        {
            get
            {
                if (_chatUsers == null)
                {
                    _chatUsers = new List<ChatUser>();
                }

                return _chatUsers;
            }
        }

        public ChatUser User { get; private set; }

        public ChatroomDto(Chatroom chatroom)
        {
            Id = chatroom.Id;
            Name = chatroom.Name;
            _chatMessages = chatroom.ChatMessages;
        }

        public void SetUser(ChatUser user)
        {
            User = user;
        }

        public void SetChatMembers(IList<ChatUser> chatUsers)
        {
            _chatUsers = chatUsers;
        }

        public string GetUsersString()
        {
            if (ChatUsers.Count == 0)
            {
                return "None";
            }
            var names = ChatUsers.Select(u => u.UserName).ToList();
            return string.Join(", ", names);
        }
    }
}
