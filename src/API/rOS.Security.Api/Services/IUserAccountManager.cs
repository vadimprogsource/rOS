using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using rOS.Security.Api.Accounts;
using rOS.Security.Api.Permissions;

namespace rOS.Security.Api.Services;

public interface IUserAccountManager 
{
    Task<IUserAccount> CreateUserAsync(Guid? ownerGuid, string login,string password);
    Task<IUserAccount> ChangeLoginAsync(IUserAccount user, IUserLogin login);
    Task<IUserAccount> ChangeEmailAsync(IUserAccount user, string email);
    Task<IUserAccount> ChangeCellular(IUserAccount user, string email);
    Task<IUserAccount> GrantUserRoleAsync(IUserAccount user, IAccessRole role);
    Task<IUserAccount> RevokeUserRoleAsync(IUserAccount user, IAccessRole role);
    Task<IUserAccount> EnableUserAccountAsync (IUserAccount user);
    Task<IUserAccount> DisableUserAccountAsync(IUserAccount user);

    Task<bool> RemoveUserAsync(IUserAccount user);
}
