using SignalRChat.Areas.Chat.Models;
using SignalRChat.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SignalRChat.Areas.Chat.Data
{
    public interface IChatroomRepository : IRepository<Chatroom>
    {
    }
}
