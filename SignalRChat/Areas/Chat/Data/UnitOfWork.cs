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
                return CreateIfNull(_chatroomRepository, typeof(ChatroomRepository));
            }
        }

        private IChatMessageRepository _chatMessageRepository;
        public IChatMessageRepository ChatMessageRepository
        {
            get
            {
                return CreateIfNull(_chatMessageRepository, typeof(ChatMessageRepository));
            }
        }

        private IChatUserRepository _chatUserRepository;
        public IChatUserRepository ChatUserRepository
        {
            get
            {
                return CreateIfNull(_chatUserRepository, typeof(ChatUserRepository));
            }
        }

        private IChatUserClaimRepository _chatUserClaimRepository;
        public IChatUserClaimRepository ChatUserClaimRepository
        {
            get
            {
                return CreateIfNull(_chatUserClaimRepository, typeof(ChatUserClaimRepository));
            }
        }

        private IChatUserRoomRepository _chatUserRoomRepository;
        public IChatUserRoomRepository ChatUserRoomRepository
        {
            get
            {
                return CreateIfNull(_chatUserRoomRepository, typeof(ChatUserRoomRepository));
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
