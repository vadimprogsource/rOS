using System;
using System.CoreLib.Builders;
using rOS.Security.Api.Accounts;
using rOS.Security.Api.Storages;

namespace rOS.Uac.InMemory;

internal class DataUserAccountStorage : Dictionary<Guid , DataUserAccount> , IUserAccountStorage
{

    public IUserAccount GetEmptyUser() => DataUserAccount.Empty;


    public Task<IUserAccount> GetUserAsync(Guid guid)
    {
        if (TryGetValue(guid, out DataUserAccount? user) && user !=null)
        {
            return Task.FromResult<IUserAccount>( user) ;
        }

        return Task.FromResult(DataUserAccount.Empty); 
    }


    public Task<bool> PutUserAsync(IUserAccount user)
    {
        this[user.Guid] = new DataUserAccount(user);
        return Task.FromResult(true);
    }



    public Task<bool> PutPasswordAsync(IUserAccount user, IUserPassword password)
    {
        if (TryGetValue(user.Guid , out DataUserAccount? userAccount) && user != null)
        {
            userAccount.Login = password.Name;
            userAccount.PasswordGuid = password.Guid;
            userAccount.PasswordHash = password.Hash;

            return Task.FromResult(true);
        }

        return Task.FromResult(false);

    }




    public Task<bool> DeleteLoginAsync(IUserAccount user, string login)
    {
        throw new NotImplementedException();
    }

    public Task<bool> DeleteUserAsync(IUserAccount user)
    {
       return Task.FromResult(  Remove(user.Guid));
    }

    public Task<IUserAccount> FindUserAsync(string login)
    {
        DataUserAccount? userAccount = Values.FirstOrDefault(x => x.Login==login);

        if (userAccount == null)
        {
            return Task.FromResult(DataUserAccount.Empty);
        }

        return Task.FromResult<IUserAccount>( userAccount);
    }

    public Task<IUserAccount> FindUserAsync(IUserPassword password)
    {

        DataUserAccount? userAccount = Values.FirstOrDefault(x => x.PasswordGuid == password.Guid && x.Login == password.Name && PasswordBuilder.Compare(x.PasswordHash , password.Hash));

        if (userAccount == null)
        {
            return Task.FromResult( DataUserAccount.Empty);
        }

        return Task.FromResult<IUserAccount>(userAccount);
    }

    public async IAsyncEnumerable<IUserAccount> FindUsersAsync(string searchPattern, int maxCount)
    {
        await Task.Delay(1);

        foreach (DataUserAccount userAccount in Values.Where(x => x.Login.Contains(searchPattern) || (x.Title != null && x.Title.Contains(searchPattern))).Take(maxCount).ToArray())
        {
            yield return userAccount;
        }
    }



    public Task<bool> PatchUserAsync(IUserAccount user)
    {
        if (TryGetValue(user.Guid, out DataUserAccount? userAccount) && userAccount != null)
        {
            userAccount.Cellular = user.Cellular;
            userAccount.Email = user.Email;
            userAccount.Title = user.Title;
            return Task.FromResult(true);
        }
        return Task.FromResult(false);
    }


}

