using SignalRChat.Areas.Identity.Models;
using SignalRChat.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SignalRChat.Areas.Identity.Data
{
    public interface IChatUserRepository : IRepository<ChatUser>
    {

    }
}
