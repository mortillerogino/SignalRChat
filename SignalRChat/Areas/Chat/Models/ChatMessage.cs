using SignalRChat.Areas.Identity.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace SignalRChat.Areas.Chat.Models
{
    public class ChatMessage
    {
        [Key]
        public int Id { get; set; }
        public DateTime TimeStamp { get; set; }
        public string Message { get; set; }

        [ForeignKey("ChatUser")]
        public int ChatUserId { get; set; }
        public ChatUser ChatUser { get; set; }

        [ForeignKey("Chatroom")]
        public int ChatroomId { get; set; }
        public Chatroom Chatroom { get; set; }
        [NotMapped]
        public string TimeStampString
        {
            get
            {
                return TimeStamp.ToString();
            }
        }
    }
}
