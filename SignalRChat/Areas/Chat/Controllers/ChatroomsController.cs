﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SignalRChat.Areas.Chat.Data;
using SignalRChat.Areas.Chat.Models;
using SignalRChat.Models;

namespace SignalRChat.Areas.Chat.Controllers
{
    [Area("Chat")]
    public class ChatroomsController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public ChatroomsController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        // GET: Chat/Chatrooms
        public async Task<IActionResult> Index()
        {
            return View(await _unitOfWork.ChatroomRepository.GetAsync());
        }

        // GET: Chat/Chatrooms/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var chatroom = await _unitOfWork.ChatroomRepository.GetByIdAsync(id);
            if (chatroom == null)
            {
                return NotFound();
            }

            return View(chatroom);
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
                await _unitOfWork.ChatroomRepository.InsertAsync(chatroom);
                await _unitOfWork.CommitAsync();
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

            var chatroom = await _unitOfWork.ChatroomRepository.GetByIdAsync(id);
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
                try
                {
                    _unitOfWork.ChatroomRepository.Update(chatroom);
                    await _unitOfWork.CommitAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    var chatroomExists = await ChatroomExists(id);
                    if (!chatroomExists)
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
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

            var chatroom = await _unitOfWork.ChatroomRepository.GetByIdAsync(id);
            if (chatroom == null)
            {
                return NotFound();
            }

            return View(chatroom);
        }

        // POST: Chat/Chatrooms/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _unitOfWork.ChatroomRepository.DeleteAsync(id);
            await _unitOfWork.CommitAsync();
            return RedirectToAction(nameof(Index));
        }

        private async Task<bool> ChatroomExists(int id)
        {
            var chatroom = await _unitOfWork.ChatroomRepository.GetByIdAsync(id);
            return chatroom != null;
        }
    }
}
