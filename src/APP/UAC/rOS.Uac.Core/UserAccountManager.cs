using System;
using System.CoreLib.Builders;
using System.CoreLib.Lockers;
using Oql.Api.Runtime;
using rOS.Security.Api.Accounts;
using rOS.Security.Api.Permissions;
using rOS.Security.Api.Services;
using rOS.Security.Api.Storages;
using rOS.Security.Entity.Accounts;

namespace rOS.Uac.Core;

public class UserAccountManager : IUserAccountManager
{

    private readonly IFactory<UserAccount> _factory;
    private readonly IUserAccountStorage   _storage;
    private readonly IInstanceProvider<IUserAccount, UserAccount> _provider;


    public UserAccountManager(IFactory<UserAccount> factory, IInstanceProvider<IUserAccount, UserAccount> provider , IUserAccountStorage storage)
    {
        _factory = factory;
        _storage = storage;
        _provider = provider;
    }


    public async Task<IUserAccount> CreateUserAsync(Guid? ownerGuid, string login, string password)
    {
        await using (new AsyncLocker<UserAccount>(login))
        {

            UserPassword userPassword = new(login, password);

            Guid passwordGuid = PasswordBuilder.MakeGuid(login, password);

            if ((await _storage.FindUserAsync(login)).IsValid)
            {
                throw new Exception("UserLoginIncorrect");
            }

            UserAccount userAccount = await _factory.CreateInstance();
            userAccount.Guid = Guid.NewGuid();
            userAccount.Login = login;
            userAccount.PasswordGuid = userPassword.Guid;
            userAccount.PasswordHash = userPassword.Hash;
            userAccount.Blocked = true;
            userAccount.RoleType = AccessRoleType.User;
            userAccount.OwnerGuid = ownerGuid;
            userAccount.CreatedOn = DateTime.Now;

            if (await _storage.PutUserAsync(userAccount))
            {
                return userAccount;
            }
            else
            {
                return _storage.GetEmptyUser();
            }
        }
    }


    public async Task<IUserAccount> ChangeCellular(IUserAccount user, string cellular) => await ChangeUserProperty(user, x => x.Cellular = cellular);



    private async Task<IUserAccount> ChangeUserProperty(IUserAccount user, Action<UserAccount> setter)
    {
        UserAccount userAccount = _provider.GetInstance(user);
        setter(userAccount);

        if (await _storage.PatchUserAsync(userAccount))
        {
            return userAccount;
        }

        return _storage.GetEmptyUser();
    }


    public async Task<IUserAccount> ChangeEmailAsync(IUserAccount user, string email) => await ChangeUserProperty(user, x => x.Email = email);
 
    public async Task<IUserAccount> ChangeLoginAsync(IUserAccount user, IUserLogin login)
    {
        if (await _storage.PutPasswordAsync(user, new UserPassword(login)))
        {
            return user;
        }

        return _storage.GetEmptyUser();
    }

    public async Task<IUserAccount> DisableUserAccountAsync(IUserAccount user) => await ChangeUserProperty(user, x => x.Blocked = true);
    
    public async Task<IUserAccount> EnableUserAccountAsync(IUserAccount user) => await ChangeUserProperty(user, x => x.Blocked = false);
   
    public Task<IUserAccount> GrantUserRoleAsync(IUserAccount user, IAccessRole role)
    {
        throw new NotImplementedException();
    }

    public async Task<bool> RemoveUserAsync(IUserAccount user)
    {

        if (user.Blocked)
        {
            return await _storage.DeleteUserAsync(user);
        }

        await DisableUserAccountAsync(user);
        return true;
    }

    public Task<IUserAccount> RevokeUserRoleAsync(IUserAccount user, IAccessRole role)
    {
        throw new NotImplementedException();
    }
}

