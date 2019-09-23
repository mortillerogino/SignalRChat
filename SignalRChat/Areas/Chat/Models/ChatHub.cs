using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.SignalR;
using SignalRChat.Areas.Chat.Data;
using SignalRChat.Areas.Identity.Models;
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

        public async Task SendMessage(int chatroomId, int userId, string message)
        {
            var user = await _unitOfWork.ChatUserRepository.GetByIdAsync(userId);

            var datetime = DateTime.Now;
            var newMesage = new ChatMessage
            {
                Username = user.UserName,
                ChatroomId = chatroomId,
                Message = message,
                TimeStamp = datetime
            };

            await _unitOfWork.ChatMessageRepository.InsertAsync(newMesage);
            await _unitOfWork.CommitAsync();
            await Clients.All.SendAsync(chatroomId.ToString(), newMesage.Username, newMesage.TimeStampString, message);
        }
    }
}
