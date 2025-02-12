using System;
using rOS.Security.Api.Accounts;
using rOS.Security.Api.Providers;
using rOS.Security.Api.Storages;
using rOS.Security.Entity.Accounts;

namespace rOS.Uac.Core;

public class UserProvider :  IUserAccountProvider
{
    private readonly IUserAccountStorage _storage;

    public UserProvider(IUserAccountStorage storage) => _storage = storage;

    public async Task<IUserAccount> FindUserAsync(string login, string password)
    {
        UserPassword   userPassword = new(login, password);
        return await _storage.FindUserAsync(login, userPassword);     
    }

    public async Task<IUserAccount[]> FindUsersAsync(string searchPattern, int maxCount)
    {
        return (await _storage.FindUsersAsync(searchPattern, maxCount)).ToArray();
    }

    public async Task<IUserAccount> GetUserAsync(Guid guid)
    {
        return await _storage.GetUserAsync(guid);
    }
}

