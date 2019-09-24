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
    public class ChatroomService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly UserManager<ChatUser> _userManager;

        public ChatroomService(IUnitOfWork unitOfWork, UserManager<ChatUser> userManager)
        {
            _unitOfWork = unitOfWork;
            _userManager = userManager;
        }

        public async Task<ChatroomDto> GetChatroomDetailsAsync(int chatroomId, ClaimsPrincipal claimsPrincipalUser)
        {
            var chatroomsThatMatchId = await _unitOfWork.ChatroomRepository.GetAsync(a => a.Id == chatroomId, null);
            var currentChatroom = chatroomsThatMatchId.FirstOrDefault();

            if (currentChatroom == null)
            {
                return null;
            }

            var messages = await _unitOfWork.ChatMessageRepository.GetAsync(a => a.ChatroomId == chatroomId, a => a.OrderByDescending(o => o.TimeStamp), a => a.ChatUser);
            var currentUser = await _userManager.GetUserAsync(claimsPrincipalUser);
            var members = _unitOfWork.ChatUserRoomRepository.Query(ur => ur.ChatroomId == chatroomId).Select(a => a.ChatUser).ToList();

            var dto = new ChatroomDto(currentChatroom);
            dto.SetUser(currentUser);
            dto.SetChatMembers(members);


            return dto;
        }

        public async Task CreateChatroom(Chatroom chatroom, ClaimsPrincipal claimsPrincipalUser)
        {
            await _unitOfWork.ChatroomRepository.InsertAsync(chatroom);
            await _unitOfWork.CommitAsync();

            var user = await _userManager.GetUserAsync(claimsPrincipalUser);
            var userRoomRelationship = new ChatUserRoom
            {
                ChatUserId = user.Id,
                ChatroomId = chatroom.Id
            };

            await _unitOfWork.ChatUserRoomRepository.InsertAsync(userRoomRelationship);
            await _unitOfWork.CommitAsync();
        }
    }
}
