using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using rOS.Security.Api.Accounts;

namespace rOS.Security.Api.Storages;

public interface IUserAccountStorage
{
    Task<IUserAccount> GetUserAsync (Guid guid);
    Task<IUserAccount> FindUserAsync(string login);
    Task<IUserAccount> FindUserAsync(IUserPassword password);
    IAsyncEnumerable<IUserAccount> FindUsersAsync(string searchPattern, int maxCount);

    Task<bool> PutPasswordAsync(IUserAccount user ,IUserPassword password);

    Task<bool>  PutUserAsync   (IUserAccount user);
    Task<bool>  PatchUserAsync(IUserAccount user);
    Task<bool>  DeleteUserAsync(IUserAccount user);

    IUserAccount GetEmptyUser();
}
