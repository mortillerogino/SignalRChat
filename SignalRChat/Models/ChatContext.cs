using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SignalRChat.Areas.Chat.Models;
using SignalRChat.Areas.Identity.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SignalRChat.Models
{
    public class ChatContext : IdentityDbContext<ChatUser,
        IdentityRole<int>, int,
        ChatUserClaim,
        IdentityUserRole<int>,
        IdentityUserLogin<int>,
        IdentityRoleClaim<int>,
        IdentityUserToken<int>>
    {
        public ChatContext(DbContextOptions dbContextOptions)
            : base(dbContextOptions)
        {

        }

        public DbSet<Chatroom> Chatrooms { get; set; }
        public DbSet<ChatMessage> ChatMessages { get; set; }
        public DbSet<ChatUserRoom> ChatUserRooms { get; set; }

        public DbSet<ChatUser> ChatUsers { get; set; }
        public DbSet<ChatUserClaim> ChatUserClaims { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Chatroom>().ToTable("Chatroom");
            modelBuilder.Entity<ChatMessage>().ToTable("ChatMessage");
            modelBuilder.Entity<ChatUser>().ToTable("ChatUser");
            modelBuilder.Entity<ChatUserClaim>().ToTable("ChatUserClaim");
            modelBuilder.Entity<ChatUserRoom>().ToTable("ChatUserRoom");
        }
    }
}
