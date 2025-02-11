using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using rOS.Security.Api.Accounts;

namespace rOS.Security.Api.Providers;

public interface IUserAuthProvider
{
    Task<IUserAccount> GetLoginAsync(string login, string password);
    Task<IUserAccount> GetActiveUserAsync (Guid userGuid);
}
