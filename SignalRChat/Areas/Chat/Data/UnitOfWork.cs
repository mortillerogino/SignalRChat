using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SignalRChat.Areas.Chat.Data
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly DbContext _dbContext;

        public UnitOfWork(DbContext dbContext)
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
