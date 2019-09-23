using SignalRChat.Areas.Chat.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SignalRChat.Areas.Chat.Data
{
    public interface IUnitOfWork : IDisposable
    {
        IChatroomRepository ChatroomRepository { get; }
        IChatMessageRepository ChatMessageRepository { get; }

        void Commit();

        Task<int> CommitAsync();
    }
}
