using Microsoft.AspNetCore.Identity;
using SignalRChat.Areas.Chat.Data;
using SignalRChat.Areas.Chat.Models;
using SignalRChat.Areas.Identity.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace SignalRChat.Areas.Chat.Services
{
    public static class ChatroomService
    {
        public static async Task<ChatroomDto> GetChatroomDetailsAsync(IUnitOfWork unitOfWork, UserManager<ChatUser> userManager, int chatroomId, ClaimsPrincipal claimsPrincipalUser)
        {
            var chatroomsThatMatchId = await unitOfWork.ChatroomRepository.GetAsync(a => a.Id == chatroomId, null);
            var currentChatroom = chatroomsThatMatchId.FirstOrDefault();

            if (currentChatroom == null)
            {
                return null;
            }

            var messages = await unitOfWork.ChatMessageRepository.GetAsync(a => a.ChatroomId == chatroomId, null, a => a.ChatUser);
            var currentUser = await userManager.GetUserAsync(claimsPrincipalUser);

            var dto = new ChatroomDto(currentChatroom);
            dto.SetUser(currentUser);

            return dto;
        }
    }
}
