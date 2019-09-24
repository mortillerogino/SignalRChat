﻿using Microsoft.AspNetCore.Identity;
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

            var messages = await _unitOfWork.ChatMessageRepository.GetAsync(a => a.ChatroomId == chatroomId, null, a => a.ChatUser);
            var currentUser = await _userManager.GetUserAsync(claimsPrincipalUser);

            var dto = new ChatroomDto(currentChatroom);
            dto.SetUser(currentUser);

            return dto;
        }
    }
}
