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
                if (_chatroomRepository == null)
                {
                    _chatroomRepository = new ChatroomRepository(_dbContext);
                }

                return _chatroomRepository;
            }
        }

        private IChatMessageRepository _chatMessageRepository;
        public IChatMessageRepository ChatMessageRepository
        {
            get
            {
                if (_chatMessageRepository == null)
                {
                    _chatMessageRepository = new ChatMessageRepository(_dbContext);
                }

                return _chatMessageRepository;
            }
        }

        private IChatUserRepository _chatUserRepository;
        public IChatUserRepository ChatUserRepository
        {
            get
            {
                if (_chatUserRepository == null)
                {
                    _chatUserRepository = new ChatUserRepository(_dbContext);
                }

                return _chatUserRepository;
            }
        }

        private IChatUserClaimRepository _chatUserClaimRepository;
        public IChatUserClaimRepository ChatUserClaimRepository
        {
            get
            {
                if (_chatUserClaimRepository == null)
                {
                    _chatUserClaimRepository = new ChatUserClaimRepository(_dbContext);
                }

                return _chatUserClaimRepository;
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
    }
}
