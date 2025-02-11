using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using rOS.Security.Api.Accounts;

namespace rOS.Security.Api.Providers;

public interface IUserAccountProvider
{
    Task<IUserAccount> GetUserAsync (Guid userGuid);
    Task<IUserAccount[]> FindUsersAsync(string searchPattern, int maxCount);
    Task<IUserAccount> FindUserAsync(string login, string password);
  
}
