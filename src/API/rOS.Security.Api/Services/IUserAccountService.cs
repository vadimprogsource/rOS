using System;
using System.Collections.Generic;
using System.Text;
using rOS.Security.Api.Providers;

namespace rOS.Security.Api.Services;

public interface IUserAccountService
{
    public IAccessRoleProvider RoleProvider { get; }
    public IAccessRoleManager  RoleManager  { get; } 

    public IUserAccountProvider UserProvider { get; }
    public IUserAccountManager  UserManager { get; }
    public IUserAuthProvider    Auth { get; } 
}
