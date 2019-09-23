using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SignalRChat.Areas.Identity.Models
{
    public class ChatUser : IdentityUser<int>
    {
        public byte[] AvatarImage { get; set; }
    }
}
