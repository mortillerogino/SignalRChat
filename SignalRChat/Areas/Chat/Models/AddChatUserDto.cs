using SignalRChat.Areas.Identity.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SignalRChat.Areas.Chat.Models
{
    public class AddChatUserDto
    {
        public List<ChatUser> NonMembers { get; set; }

        public int MemberToAddId { get; set; }

        public Chatroom Chatroom { get; set; }

        public int ChatroomId { get; set; }

    }
}
