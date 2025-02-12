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
    Task<IUserAccount> FindUserAsync(string login, IUserPassword password);
    Task<IEnumerable<IUserAccount>> FindUsersAsync(string searchPattern, int maxCount);

    Task<bool> PutPasswordAsync(IUserAccount user ,IUserPassword password);
    Task<bool> PutLoginAsync(IUserAccount user,string  login);

    Task<bool>  PutUserAsync   (IUserAccount user);
    Task<bool>  DeleteUserAsync(IUserAccount user);

    Task<bool> DeleteLoginAsync(IUserAccount user, string login);

    IUserAccount GetEmptyUser();
}
