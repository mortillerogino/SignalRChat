using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using SignalRChat.Areas.Chat.Data;
using SignalRChat.Areas.Chat.Models;
using SignalRChat.Areas.Chat.Services;
using SignalRChat.Areas.Identity.Models;
using SignalRChat.Models;

namespace SignalRChat.Areas.Chat.Controllers
{
    [Area("Chat")]
    [Authorize]
    public class ChatroomsController : Controller
    {
        private readonly UserManager<ChatUser> _userManager;
        private readonly ChatroomService _service;

        public ChatroomsController(UserManager<ChatUser> userManager, ChatroomService service)
        {
            _userManager = userManager;
            _service = service;
        }

        // GET: Chat/Chatrooms
        public async Task<IActionResult> Index()
        {
            List<Chatroom> chatrooms = await _service.GetUserChatroomsAsync(User);

            return View(chatrooms);
        }

        

        // GET: Chat/Chatrooms/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var dto = await _service.GetChatroomDetailsAsync(chatroomId: id.Value, claimsPrincipalUser: User);

            return View(dto);
        }

        // GET: Chat/Chatrooms/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Chat/Chatrooms/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name")] Chatroom chatroom)
        {
            if (ModelState.IsValid)
            {
                await _service.CreateChatroom(chatroom, User);

                return RedirectToAction(nameof(Index));
            }

            return View(chatroom);
        }

        // GET: Chat/Chatrooms/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) 
            {
                return NotFound();
            }

            var chatroom = await _service.GetChatroomAsync(id.Value);
            if (chatroom == null)
            {
                return NotFound();
            }

            return View(chatroom);
        }

        // POST: Chat/Chatrooms/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name")] Chatroom chatroom)
        {
            if (id != chatroom.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                await _service.UpdateChatroomAsync(chatroom);

                return RedirectToAction(nameof(Index));
            }
            return View(chatroom);
        }

        

        // GET: Chat/Chatrooms/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var chatroom = await _service.GetChatroomAsync(id.Value);
            if (chatroom == null)
            {
                return NotFound();
            }

            return View(chatroom);
        }

        public async Task<IActionResult> AddMembers(int? id)
        {
            var dto = await _service.GetAddMembersDto(chatroomId: id.Value);
            
            return View(dto);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddMembers([Bind("MemberToAddId, ChatroomId")] AddChatUserDto dto)
        {
            await _service.CreateUserRoomRelationshipAsync(dto.ChatroomId, dto.MemberToAddId);

            return RedirectToAction("Details", new { id = dto.ChatroomId });
        }

        // POST: Chat/Chatrooms/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _service.DeleteChatroomAsync(id);
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Leave(int id)
        {
            await _service.LeaveChatroomAsync(chatroomId: id, User);
            return RedirectToAction(nameof(Index));
        }


    }
}
