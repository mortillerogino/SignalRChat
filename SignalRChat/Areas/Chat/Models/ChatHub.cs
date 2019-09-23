using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SignalRChat.Areas.Chat.Models
{
    public class ChatHub : Hub
    {
        public async Task SendMessage(int chatroomId, string user, string message)
        {
            var timestamp = DateTime.Now;
            await Clients.All.SendAsync(chatroomId.ToString(), user, timestamp, message);
        }
    }
}
