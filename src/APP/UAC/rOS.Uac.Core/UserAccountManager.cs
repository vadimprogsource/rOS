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


    public UserAccountManager(IFactory<UserAccount> factory , IUserAccountStorage storage)
    {
        _factory = factory;
        _storage = storage;
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

            return _storage.GetEmptyUser();
        }
    }


    public Task<IUserAccount> ChangeCellular(IUserAccount user, string email)
    {
        throw new NotImplementedException();
    }

    public Task<IUserAccount> ChangeEmail(IUserAccount user, string email)
    {
        throw new NotImplementedException();
    }

    public Task<IUserAccount> ChangePasswordAsync(IUserAccount user, string password)
    {
        throw new NotImplementedException();
    }

    public Task<IUserAccount> DisableUserAccountAsync(IUserAccount user)
    {
        throw new NotImplementedException();
    }

    public Task<IUserAccount> EnableUserAccountAsync(IUserAccount user)
    {
        throw new NotImplementedException();
    }

    public Task<IUserAccount> GrantUserRoleAsync(IUserAccount user, IAccessRole role)
    {
        throw new NotImplementedException();
    }

    public Task<bool> RemoveUserAsync(IUserAccount user)
    {
        throw new NotImplementedException();
    }

    public Task<IUserAccount> RevokeUserRoleAsync(IUserAccount user, IAccessRole role)
    {
        throw new NotImplementedException();
    }
}

