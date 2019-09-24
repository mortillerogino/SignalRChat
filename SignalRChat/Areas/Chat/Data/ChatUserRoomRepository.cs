using Microsoft.EntityFrameworkCore;
using SignalRChat.Areas.Chat.Models;
using SignalRChat.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SignalRChat.Areas.Chat.Data
{
    public class ChatUserRoomRepository : Repository<ChatUserRoom>, IChatUserRoomRepository
    {
        public ChatUserRoomRepository(DbContext dbContext)
            : base (dbContext)
        {

        }
    }
}
