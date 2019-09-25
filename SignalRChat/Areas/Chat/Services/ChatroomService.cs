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
            var currentChatroom = await GetChatroomAsync(chatroomId);

            if (currentChatroom == null)
            {
                return null;
            }

            var messages = await GetChatroomMessages(chatroomId);
            ChatUser currentUser = await GetUserAsync(claimsPrincipalUser);
            List<ChatUser> members = GetChatroomMembers(chatroomId);

            ChatroomDto dto = CreateChatroomDto(currentChatroom, currentUser, members);

            return dto;
        }

        public async Task CreateChatroom(Chatroom chatroom, ClaimsPrincipal claimsPrincipalUser)
        {
            await CreateChatroom(chatroom);

            var user = await GetUserAsync(claimsPrincipalUser);

            await CreateUserRoomRelationshipAsync(chatroom, user);
        }

        public async Task<List<Chatroom>> GetUserChatroomsAsync(ClaimsPrincipal claimsPrincipalUser)
        {
            var user = await GetUserAsync(claimsPrincipalUser);
            return await GetUserRooms(user);
        }

        public async Task UpdateChatroomAsync(Chatroom chatroom)
        {
            _unitOfWork.ChatroomRepository.Update(chatroom);
            await _unitOfWork.CommitAsync();
        }

        public async Task<AddChatUserDto> GetAddMembersDto(int chatroomId)
        {
            List<ChatUser> nonMembers = await GetChatroomNonMembersAsync(chatroomId);
            var chatroom = await GetChatroomAsync(chatroomId);

            return CreateAddMemberPageDto(nonMembers, chatroom);
        }

        public async Task DeleteChatroomAsync(int id)
        {
            await _unitOfWork.ChatroomRepository.DeleteAsync(id);
            await _unitOfWork.CommitAsync();
        }

        public async Task LeaveChatroomAsync(int chatroomId, ClaimsPrincipal claimsPrincipal)
        {
            await DeleteUserRoomRelationshipAsync(chatroomId, claimsPrincipal);

            List<ChatUserRoom> membersRemaining = await GetUserRoomRelationshipsAsync(chatroomId);
            if (membersRemaining.Count == 0)
            {
                await DeleteChatroomAsync(chatroomId);
            }

            await _unitOfWork.CommitAsync();
        }

        private async Task<List<ChatUserRoom>> GetUserRoomRelationshipsAsync(int chatroomId)
        {
            return await _unitOfWork.ChatUserRoomRepository.GetAsync(ur => ur.ChatroomId == chatroomId);
        }

        private async Task DeleteUserRoomRelationshipAsync(int chatroomId, ClaimsPrincipal claimsPrincipal)
        {
            var user = await GetUserAsync(claimsPrincipal);
            var relationship = await GetUserRoomRelationshipAsync(chatroomId, user);
            await DeleteUserRoomRelationshipAsync(relationship);
        }

        private async Task DeleteUserRoomRelationshipAsync(ChatUserRoom relationship)
        {
            await _unitOfWork.ChatUserRoomRepository.DeleteAsync(relationship.Id);
            await _unitOfWork.CommitAsync();
        }

        private async Task<ChatUserRoom> GetUserRoomRelationshipAsync(int chatroomId, ChatUser user)
        {
            return await _unitOfWork.ChatUserRoomRepository.GetFirstOrDefaultAsync(ur => ur.ChatroomId == chatroomId && ur.ChatUserId == user.Id);
        }

        private static AddChatUserDto CreateAddMemberPageDto(List<ChatUser> nonMembers, Chatroom chatroom)
        {
            var dto = new AddChatUserDto
            {
                NonMembers = nonMembers,
                Chatroom = chatroom
            };

            return dto;
        }

        private async Task<List<ChatUser>> GetChatroomNonMembersAsync(int chatroomId)
        {
            var nonMembers = new List<ChatUser>();
            var members = await _unitOfWork.ChatUserRepository.GetAsync();
            foreach (ChatUser user in members)
            {
                var relationshipWithRoom = await _unitOfWork.ChatUserRoomRepository.GetAsync(ur => ur.ChatUserId == user.Id && ur.ChatroomId == chatroomId);
                if (relationshipWithRoom.Count > 0)
                {
                    continue;
                }
                nonMembers.Add(user);
            }

            return nonMembers;
        }

        private async Task<List<Chatroom>> GetUserRooms(ChatUser user)
        {
            var userRooms = _unitOfWork.ChatUserRoomRepository.Query(ur => ur.ChatUserId == user.Id)
                            .Select(a => a.ChatroomId);

            var chatrooms = await _unitOfWork.ChatroomRepository.GetAsync(r => userRooms.Contains(r.Id));
            return chatrooms;
        }

        private static ChatroomDto CreateChatroomDto(Chatroom currentChatroom, ChatUser currentUser, List<ChatUser> members)
        {
            var dto = new ChatroomDto(currentChatroom);
            dto.SetUser(currentUser);
            dto.SetChatMembers(members);
            return dto;
        }

        private List<ChatUser> GetChatroomMembers(int chatroomId)
        {
            return _unitOfWork.ChatUserRoomRepository.Query(ur => ur.ChatroomId == chatroomId).Select(a => a.ChatUser).ToList();
        }

        private async Task<ChatUser> GetUserAsync(ClaimsPrincipal claimsPrincipalUser)
        {
            return await _userManager.GetUserAsync(claimsPrincipalUser);
        }
        

        public async Task CreateUserRoomRelationshipAsync(Chatroom chatroom, ChatUser user)
        {
            var userRoomRelationship = new ChatUserRoom
            {
                ChatUserId = user.Id,
                ChatroomId = chatroom.Id
            };

            await _unitOfWork.ChatUserRoomRepository.InsertAsync(userRoomRelationship);
            await _unitOfWork.CommitAsync();
        }

        public async Task CreateUserRoomRelationshipAsync(int chatroomId, int userId)
        {
            var userRoomRelationship = new ChatUserRoom
            {
                ChatUserId = userId,
                ChatroomId = chatroomId
            };

            await _unitOfWork.ChatUserRoomRepository.InsertAsync(userRoomRelationship);
            await _unitOfWork.CommitAsync();
        }

        private async Task CreateChatroom(Chatroom chatroom)
        {
            await _unitOfWork.ChatroomRepository.InsertAsync(chatroom);
            await _unitOfWork.CommitAsync();
        }

        public async Task<Chatroom> GetChatroomAsync(int chatroomId)
        {
            return await _unitOfWork.ChatroomRepository.GetByIdAsync(chatroomId);
        }

        private async Task<IEnumerable<ChatMessage>> GetChatroomMessages(int chatroomId)
        {
            return await _unitOfWork.ChatMessageRepository.GetAsync(a => a.ChatroomId == chatroomId, a => a.OrderByDescending(o => o.TimeStamp), a => a.ChatUser);
        }

    }
}
