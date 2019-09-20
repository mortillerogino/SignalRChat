using Microsoft.EntityFrameworkCore;
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


    }
}
