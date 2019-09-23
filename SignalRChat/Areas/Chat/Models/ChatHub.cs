using Microsoft.AspNetCore.SignalR;
using SignalRChat.Areas.Chat.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SignalRChat.Areas.Chat.Models
{
    public class ChatHub : Hub
    {
        private readonly IUnitOfWork _unitOfWork;

        public ChatHub(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task SendMessage(int chatroomId, string user, string message)
        {
            var datetime = DateTime.Now;
            var newMesage = new ChatMessage
            {
                Username = user,
                ChatroomId = chatroomId,
                Message = message,
                TimeStamp = datetime
            };

            await _unitOfWork.ChatMessageRepository.InsertAsync(newMesage);
            await _unitOfWork.CommitAsync();
            await Clients.All.SendAsync(chatroomId.ToString(), user, newMesage.TimeStampString, message);
        }
    }
}
