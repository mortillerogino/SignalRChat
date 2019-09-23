using Microsoft.EntityFrameworkCore;
using SignalRChat.Areas.Chat.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SignalRChat.Data
{
    public class ChatMessageRepository : Repository<ChatMessage>, IChatMessageRepository
    {
        public ChatMessageRepository(DbContext dbContext) 
            : base (dbContext)
        {

        }
    }
}
