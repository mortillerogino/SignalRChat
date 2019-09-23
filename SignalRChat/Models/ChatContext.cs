using Microsoft.EntityFrameworkCore;
using SignalRChat.Areas.Chat.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SignalRChat.Models
{
    public class ChatContext : DbContext
    {
        public ChatContext(DbContextOptions dbContextOptions)
            : base(dbContextOptions)
        {

        }

        public DbSet<Chatroom> Chatrooms { get; set; }
        public DbSet<ChatMessage> ChatMessages { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Chatroom>().ToTable("Chatroom");
            modelBuilder.Entity<ChatMessage>().ToTable("ChatMessage");
        }
    }
}
