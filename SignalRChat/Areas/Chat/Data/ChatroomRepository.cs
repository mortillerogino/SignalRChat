using Microsoft.EntityFrameworkCore;
using SignalRChat.Areas.Chat.Models;
using SignalRChat.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SignalRChat.Areas.Chat.Data
{
    public class ChatroomRepository : Repository<Chatroom>, IChatroomRepository
    {
        public ChatroomRepository(DbContext dbContext) 
            : base(dbContext)
        {

        }
    }
}
