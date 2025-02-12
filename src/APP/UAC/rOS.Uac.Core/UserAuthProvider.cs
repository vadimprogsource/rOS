using System;
using rOS.Security.Api.Accounts;
using rOS.Security.Api.Providers;
using rOS.Security.Api.Storages;
using rOS.Security.Entity.Accounts;

namespace rOS.Uac.Core
{
    public class UserAuthProvider : IUserAuthProvider
    {
        private readonly IUserAccountStorage _storage;

        public UserAuthProvider(IUserAccountStorage storage) => _storage = storage;
        

        public async Task<IUserAccount> GetActiveUserAsync(Guid userGuid)
        {
            IUserAccount user = await _storage.GetUserAsync(userGuid);
            return user.Blocked ? _storage.GetEmptyUser() : user;
        }

        public async Task<IUserAccount> GetLoginAsync(string login, string password)
        {
            UserPassword userPassword = new(login, password);
            IUserAccount user =  await _storage.FindUserAsync(login, userPassword);
            return user.Blocked ? _storage.GetEmptyUser() : user;
        }
    }
}

