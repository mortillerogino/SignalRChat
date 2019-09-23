using Microsoft.AspNetCore.Identity;
using SignalRChat.Areas.Chat.Data;
using SignalRChat.Areas.Identity.Models;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;

namespace SignalRChat.Areas.Identity.Data
{
    public class ChatUserStore : IUserStore<ChatUser>,
        IUserPasswordStore<ChatUser>,
        IUserEmailStore<ChatUser>,
        IUserSecurityStampStore<ChatUser>,
        IUserClaimStore<ChatUser>

    {
        private bool _disposedValue = false;
        private readonly IUnitOfWork _unitOfWork;

        public ChatUserStore(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IdentityResult> CreateAsync(ChatUser user, CancellationToken cancellationToken)
        {
            try
            {
                cancellationToken.ThrowIfCancellationRequested();
                ThrowIfDisposed();
                await _unitOfWork.ChatUserRepository.InsertAsync(user);
                await _unitOfWork.CommitAsync();
                return IdentityResult.Success;
            }
            catch (Exception ex)
            {
                var error = GetErrors(ex);
                return IdentityResult.Failed(error);
            }
        }

        public async Task<IdentityResult> DeleteAsync(ChatUser user, CancellationToken cancellationToken)
        {
            try
            {
                cancellationToken.ThrowIfCancellationRequested();
                ThrowIfDisposed();
                await _unitOfWork.ChatUserRepository.DeleteAsync(user.Id);
                await _unitOfWork.CommitAsync();
                return IdentityResult.Success;
            }
            catch (Exception ex)
            {
                var error = GetErrors(ex);
                return IdentityResult.Failed(error);
            }
        }

        public async Task<ChatUser> FindByIdAsync(string userId, CancellationToken cancellationToken)
        {
            try
            {
                cancellationToken.ThrowIfCancellationRequested();
                ThrowIfDisposed();
                var applicationUser = await _unitOfWork.ChatUserRepository.GetByIdAsync(userId);
                return applicationUser;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<ChatUser> FindByNameAsync(string normalizedUserName, CancellationToken cancellationToken)
        {
            try
            {
                cancellationToken.ThrowIfCancellationRequested();
                ThrowIfDisposed();
                var applicationUser = await _unitOfWork.ChatUserRepository.GetFirstOrDefaultAsync(u => u.NormalizedUserName == normalizedUserName);
                return applicationUser;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<string> GetNormalizedUserNameAsync(ChatUser user, CancellationToken cancellationToken)
        {
            try
            {
                cancellationToken.ThrowIfCancellationRequested();
                ThrowIfDisposed();
                var applicationUser = await _unitOfWork.ChatUserRepository.GetByIdAsync(user.Id);
                return applicationUser.NormalizedUserName;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<string> GetUserIdAsync(ChatUser user, CancellationToken cancellationToken)
        {
            try
            {
                cancellationToken.ThrowIfCancellationRequested();
                ThrowIfDisposed();
                var applicationUser = await _unitOfWork.ChatUserRepository.GetByIdAsync(user.Id);
                if (applicationUser != null)
                {
                    return applicationUser.Id.ToString();
                }
                else
                {
                    return user.Id.ToString();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<string> GetUserNameAsync(ChatUser user, CancellationToken cancellationToken)
        {
            try
            {
                cancellationToken.ThrowIfCancellationRequested();
                ThrowIfDisposed();
                var applicationUser = await _unitOfWork.ChatUserRepository.GetFirstOrDefaultAsync(u => u.Id == user.Id);
                if (applicationUser != null)
                {
                    return applicationUser.UserName;
                }
                else
                {
                    return user.UserName;
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task SetNormalizedUserNameAsync(ChatUser user, string normalizedName, CancellationToken cancellationToken)
        {
            try
            {
                cancellationToken.ThrowIfCancellationRequested();
                ThrowIfDisposed();
                var applicationUser = await _unitOfWork.ChatUserRepository.GetFirstOrDefaultAsync(u => u.Id == user.Id);
                if (applicationUser != null)
                {
                    applicationUser.NormalizedUserName = normalizedName;
                    _unitOfWork.ChatUserRepository.Update(applicationUser);
                    await _unitOfWork.CommitAsync();
                }
                else
                {
                    user.NormalizedUserName = normalizedName;
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task SetUserNameAsync(ChatUser user, string userName, CancellationToken cancellationToken)
        {
            try
            {
                cancellationToken.ThrowIfCancellationRequested();
                ThrowIfDisposed();
                var applicationUser = await _unitOfWork.ChatUserRepository.GetFirstOrDefaultAsync(u => u.Id == user.Id);

                if (applicationUser != null)
                {
                    applicationUser.UserName = userName;
                    _unitOfWork.ChatUserRepository.Update(applicationUser);
                    await _unitOfWork.CommitAsync();
                }
                else
                {
                    user.UserName = userName;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<IdentityResult> UpdateAsync(ChatUser user, CancellationToken cancellationToken)
        {
            try
            {
                cancellationToken.ThrowIfCancellationRequested();
                ThrowIfDisposed();
                _unitOfWork.ChatUserRepository.Update(user);
                await _unitOfWork.CommitAsync();
                return IdentityResult.Success;
            }
            catch (Exception ex)
            {
                var error = GetErrors(ex);
                return IdentityResult.Failed(error);
            }
        }

        public async Task SetPasswordHashAsync(ChatUser user, string passwordHash, CancellationToken cancellationToken)
        {
            try
            {
                cancellationToken.ThrowIfCancellationRequested();
                ThrowIfDisposed();
                var applicationUser = await _unitOfWork.ChatUserRepository.GetFirstOrDefaultAsync(u => u.Id == user.Id);
                if (applicationUser != null)
                {
                    applicationUser.PasswordHash = passwordHash;
                    _unitOfWork.ChatUserRepository.Update(applicationUser);
                    await _unitOfWork.CommitAsync();
                }
                else
                {
                    user.PasswordHash = passwordHash;
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<string> GetPasswordHashAsync(ChatUser user, CancellationToken cancellationToken)
        {
            try
            {
                cancellationToken.ThrowIfCancellationRequested();
                ThrowIfDisposed();
                var applicationUser = await _unitOfWork.ChatUserRepository.GetFirstOrDefaultAsync(u => u.Id == user.Id);
                if (applicationUser != null)
                {
                    return applicationUser.PasswordHash;
                }
                else
                {
                    return user.PasswordHash;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<bool> HasPasswordAsync(ChatUser user, CancellationToken cancellationToken)
        {
            try
            {
                cancellationToken.ThrowIfCancellationRequested();
                ThrowIfDisposed();
                var applicationUser = await _unitOfWork.ChatUserRepository.GetFirstOrDefaultAsync(u => u.Id == user.Id);
                if (applicationUser != null)
                {
                    return !string.IsNullOrEmpty(applicationUser.PasswordHash);
                }
                else
                {
                    return !string.IsNullOrEmpty(user.PasswordHash);
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task SetEmailAsync(ChatUser user, string email, CancellationToken cancellationToken)
        {
            try
            {
                cancellationToken.ThrowIfCancellationRequested();
                ThrowIfDisposed();
                var applicationUser = await _unitOfWork.ChatUserRepository.GetFirstOrDefaultAsync(u => u.Id == user.Id);
                if (applicationUser != null)
                {
                    applicationUser.Email = email;
                    _unitOfWork.ChatUserRepository.Update(applicationUser);
                    await _unitOfWork.CommitAsync();
                }
                else
                {
                    user.Email = email;
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<string> GetEmailAsync(ChatUser user, CancellationToken cancellationToken)
        {
            try
            {
                cancellationToken.ThrowIfCancellationRequested();
                ThrowIfDisposed();
                var applicationUser = await _unitOfWork.ChatUserRepository.GetFirstOrDefaultAsync(u => u.Id == user.Id);
                if (applicationUser != null)
                {
                    return applicationUser.Email;
                }
                else
                {
                    return user.Email;
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<bool> GetEmailConfirmedAsync(ChatUser user, CancellationToken cancellationToken)
        {
            try
            {
                cancellationToken.ThrowIfCancellationRequested();
                ThrowIfDisposed();
                var applicationUser = await _unitOfWork.ChatUserRepository.GetFirstOrDefaultAsync(u => u.Id == user.Id);
                if (applicationUser != null)
                {
                    return applicationUser.EmailConfirmed;
                }
                else
                {
                    return user.EmailConfirmed;
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task SetEmailConfirmedAsync(ChatUser user, bool confirmed, CancellationToken cancellationToken)
        {
            try
            {
                cancellationToken.ThrowIfCancellationRequested();
                ThrowIfDisposed();
                var applicationUser = await _unitOfWork.ChatUserRepository.GetFirstOrDefaultAsync(u => u.Id == user.Id);
                if (applicationUser != null)
                {
                    applicationUser.EmailConfirmed = confirmed;
                    _unitOfWork.ChatUserRepository.Update(applicationUser);
                    await _unitOfWork.CommitAsync();
                }
                else
                {
                    user.EmailConfirmed = confirmed;
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<ChatUser> FindByEmailAsync(string normalizedEmail, CancellationToken cancellationToken)
        {
            try
            {
                cancellationToken.ThrowIfCancellationRequested();
                ThrowIfDisposed();
                var applicationUser = await _unitOfWork.ChatUserRepository.GetFirstOrDefaultAsync(u => u.NormalizedEmail == normalizedEmail);
                return applicationUser;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<string> GetNormalizedEmailAsync(ChatUser user, CancellationToken cancellationToken)
        {
            try
            {
                cancellationToken.ThrowIfCancellationRequested();
                ThrowIfDisposed();
                var applicationUser = await _unitOfWork.ChatUserRepository.GetFirstOrDefaultAsync(u => u.Id == user.Id);
                if (applicationUser != null)
                {
                    return applicationUser.NormalizedEmail;
                }
                else
                {
                    return user.NormalizedEmail;
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task SetNormalizedEmailAsync(ChatUser user, string normalizedEmail, CancellationToken cancellationToken)
        {
            try
            {
                cancellationToken.ThrowIfCancellationRequested();
                ThrowIfDisposed();
                var applicationUser = await _unitOfWork.ChatUserRepository.GetFirstOrDefaultAsync(u => u.Id == user.Id);
                if (applicationUser != null)
                {
                    applicationUser.NormalizedEmail = normalizedEmail;
                    _unitOfWork.ChatUserRepository.Update(applicationUser);
                    await _unitOfWork.CommitAsync();
                }
                else
                {
                    user.NormalizedEmail = normalizedEmail;
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task SetSecurityStampAsync(ChatUser user, string stamp, CancellationToken cancellationToken)
        {
            try
            {
                cancellationToken.ThrowIfCancellationRequested();
                ThrowIfDisposed();
                var applicationUser = await _unitOfWork.ChatUserRepository.GetFirstOrDefaultAsync(u => u.Id == user.Id);
                if (applicationUser != null)
                {
                    applicationUser.SecurityStamp = stamp;
                    _unitOfWork.ChatUserRepository.Update(applicationUser);
                    await _unitOfWork.CommitAsync();
                }
                else
                {
                    user.SecurityStamp = stamp;
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<string> GetSecurityStampAsync(ChatUser user, CancellationToken cancellationToken)
        {
            try
            {
                cancellationToken.ThrowIfCancellationRequested();
                ThrowIfDisposed();
                var applicationUser = await _unitOfWork.ChatUserRepository.GetFirstOrDefaultAsync(u => u.Id == user.Id);
                if (applicationUser != null)
                {
                    return applicationUser.SecurityStamp;
                }
                else
                {
                    return user.SecurityStamp;
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<IList<Claim>> GetClaimsAsync(ChatUser user, CancellationToken cancellationToken)
        {
            try
            {
                cancellationToken.ThrowIfCancellationRequested();
                ThrowIfDisposed();
                var userClaims = await _unitOfWork.ChatUserClaimRepository.GetAsync(c => c.UserId == user.Id);

                var claims = new ConcurrentBag<Claim>();

                if (userClaims.Count > 0)
                {
                    Parallel.ForEach(userClaims, currentUserClaim =>
                    {
                        claims.Add(currentUserClaim.ToClaim());
                    });
                }

                return claims.ToList();

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task AddClaimsAsync(ChatUser user, IEnumerable<Claim> claims, CancellationToken cancellationToken)
        {
            try
            {
                cancellationToken.ThrowIfCancellationRequested();
                ThrowIfDisposed();

                var taskList = new List<Task>();

                foreach (Claim c in claims)
                {
                    var userClaim = new ChatUserClaim
                    {
                        UserId = user.Id,
                        ClaimType = c.Type,
                        ClaimValue = c.Value
                    };

                    taskList.Add(_unitOfWork.ChatUserClaimRepository.InsertAsync(userClaim));
                }

                await Task.WhenAll(taskList);
                await _unitOfWork.CommitAsync();
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public async Task ReplaceClaimAsync(ChatUser user, Claim claim, Claim newClaim, CancellationToken cancellationToken)
        {
            try
            {
                cancellationToken.ThrowIfCancellationRequested();
                ThrowIfDisposed();
                var matches = await _unitOfWork.ChatUserClaimRepository.GetAsync(uc => uc.UserId == user.Id && uc.ClaimType == claim.Type && uc.ClaimValue == claim.Value);

                foreach (ChatUserClaim uc in matches)
                {
                    uc.ClaimValue = newClaim.Value;
                    uc.ClaimType = newClaim.Type;
                }

                await _unitOfWork.CommitAsync();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task RemoveClaimsAsync(ChatUser user, IEnumerable<Claim> claims, CancellationToken cancellationToken)
        {
            try
            {
                cancellationToken.ThrowIfCancellationRequested();
                ThrowIfDisposed();

                var taskList = new List<Task>();

                foreach (var claim in claims)
                {
                    var matchedClaims = await _unitOfWork.ChatUserClaimRepository.GetAsync(uc => uc.UserId.Equals(user.Id) && uc.ClaimValue == claim.Value && uc.ClaimType == claim.Type);
                    foreach (var c in matchedClaims)
                    {
                        taskList.Add(_unitOfWork.ChatUserClaimRepository.DeleteAsync(c));
                    }
                }

                await Task.WhenAll(taskList);
                await _unitOfWork.CommitAsync();
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public async Task<IList<ChatUser>> GetUsersForClaimAsync(Claim claim, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            ThrowIfDisposed();

            var matchingUserClaims = await _unitOfWork.ChatUserClaimRepository.GetAsync(uc => uc.ClaimType == claim.Type && uc.ClaimValue == claim.Value);

            var taskList = new List<Task<ChatUser>>();

            foreach (ChatUserClaim uc in matchingUserClaims)
            {
                taskList.Add(_unitOfWork.ChatUserRepository.GetByIdAsync(uc.UserId));
            }

            var result = await Task.WhenAll(taskList.ToList());
            return result;
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposedValue)
            {
                if (disposing)
                {
                    _unitOfWork.Dispose();
                }

                _disposedValue = true;
            }
        }


        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        private IdentityError GetErrors(Exception ex)
        {
            var error = new IdentityError()
            {
                Code = ex.GetType().Name,
                Description = ex.Message
            };

            return error;
        }

        private void ThrowIfDisposed()
        {
            if (_disposedValue)
            {
                throw new ObjectDisposedException(GetType().Name);
            }
        }


    }
}
