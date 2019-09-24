using SignalRChat.Areas.Identity.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace SignalRChat.Areas.Chat.Models
{
    public class ChatUserRoom
    {
        [Key]
        public int Id { get; set; }

        [ForeignKey("ChatUser")]
        public int ChatUserId { get; set; }
        public ChatUser ChatUser { get; set; }

        [ForeignKey("Chatroom")]
        public int ChatroomId { get; set; }
        public Chatroom Chatroom { get; set; }
    }
}
