using Microsoft.EntityFrameworkCore;
using SignalRChat.Areas.Identity.Data;
using SignalRChat.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SignalRChat.Areas.Chat.Data
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ChatContext _dbContext;

        public UnitOfWork(ChatContext dbContext)
        {
            _dbContext = dbContext;
        }

        private IChatroomRepository _chatroomRepository;
        public IChatroomRepository ChatroomRepository
        {
            get
            {
                _chatroomRepository = CreateIfNull(_chatroomRepository, typeof(ChatroomRepository));
                return _chatroomRepository;
            }
        }

        private IChatMessageRepository _chatMessageRepository;
        public IChatMessageRepository ChatMessageRepository
        {
            get
            {
                _chatMessageRepository = CreateIfNull(_chatMessageRepository, typeof(ChatMessageRepository));
                return _chatMessageRepository;
            }
        }

        private IChatUserRepository _chatUserRepository;
        public IChatUserRepository ChatUserRepository
        {
            get
            {
                _chatUserRepository = CreateIfNull(_chatUserRepository, typeof(ChatUserRepository));
                return _chatUserRepository;
            }
        }

        private IChatUserClaimRepository _chatUserClaimRepository;
        public IChatUserClaimRepository ChatUserClaimRepository
        {
            get
            {
                _chatUserClaimRepository = CreateIfNull(_chatUserClaimRepository, typeof(ChatUserClaimRepository));
                return _chatUserClaimRepository;
            }
        }

        private IChatUserRoomRepository _chatUserRoomRepository;
        public IChatUserRoomRepository ChatUserRoomRepository
        {
            get
            {
                _chatUserRoomRepository = CreateIfNull(_chatUserRoomRepository, typeof(ChatUserRoomRepository));
                return _chatUserRoomRepository;
            }
        }

        public void Commit()
        {
            _dbContext.SaveChanges();
        }

        public async Task<int> CommitAsync()
        {
            return await _dbContext.SaveChangesAsync();
        }

        private bool _disposedValue = false; 
        protected virtual void Dispose(bool disposing)
        {
            if (!_disposedValue)
            {
                if (disposing)
                {
                    _dbContext.Dispose();
                }

                _disposedValue = true;
            }
        }

        public void Dispose()
        {
            Dispose(true);
        }

        private T CreateIfNull<T>(T privateProperty, Type repositoryClass)
        {
            if (privateProperty == null)
            {
                privateProperty = (T)Activator.CreateInstance(repositoryClass, _dbContext);
            }

            return privateProperty;
        }
    }
}
