using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SignalRChat.Areas.Identity.Models;
using SignalRChat.Models;

namespace SignalRChat.Areas.Identity.Controllers
{
    [Area("Identity")]
    public class ChatUsersController : Controller
    {
        private readonly ChatContext _context;

        public ChatUsersController(ChatContext context)
        {
            _context = context;
        }

        // GET: Identity/ChatUsers
        public async Task<IActionResult> Index()
        {
            return View(await _context.ChatUsers.ToListAsync());
        }

        // GET: Identity/ChatUsers/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var chatUser = await _context.ChatUsers
                .FirstOrDefaultAsync(m => m.Id == id);
            if (chatUser == null)
            {
                return NotFound();
            }

            return View(chatUser);
        }

        // GET: Identity/ChatUsers/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Identity/ChatUsers/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("AvatarImage,Id,UserName,NormalizedUserName,Email,NormalizedEmail,EmailConfirmed,PasswordHash,SecurityStamp,ConcurrencyStamp,PhoneNumber,PhoneNumberConfirmed,TwoFactorEnabled,LockoutEnd,LockoutEnabled,AccessFailedCount")] ChatUser chatUser)
        {
            if (ModelState.IsValid)
            {
                _context.Add(chatUser);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(chatUser);
        }

        // GET: Identity/ChatUsers/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var chatUser = await _context.ChatUsers.FindAsync(id);
            if (chatUser == null)
            {
                return NotFound();
            }
            return View(chatUser);
        }

        // POST: Identity/ChatUsers/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("AvatarImage,Id,UserName,NormalizedUserName,Email,NormalizedEmail,EmailConfirmed,PasswordHash,SecurityStamp,ConcurrencyStamp,PhoneNumber,PhoneNumberConfirmed,TwoFactorEnabled,LockoutEnd,LockoutEnabled,AccessFailedCount")] ChatUser chatUser)
        {
            if (id != chatUser.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(chatUser);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ChatUserExists(chatUser.Id))
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
            return View(chatUser);
        }

        // GET: Identity/ChatUsers/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var chatUser = await _context.ChatUsers
                .FirstOrDefaultAsync(m => m.Id == id);
            if (chatUser == null)
            {
                return NotFound();
            }

            return View(chatUser);
        }

        // POST: Identity/ChatUsers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var chatUser = await _context.ChatUsers.FindAsync(id);
            _context.ChatUsers.Remove(chatUser);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ChatUserExists(int id)
        {
            return _context.ChatUsers.Any(e => e.Id == id);
        }
    }
}
