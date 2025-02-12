using System;
using rOS.Security.Api.Providers;
using rOS.Security.Api.Services;

namespace rOS.Uac.Core;

public class UserAccountService : IUserAccountService
{
    public IUserAccountProvider UserProvider { get; }

    public IUserAccountManager UserManager { get; }

    public IUserAuthProvider Auth { get; }

    public IAccessRoleProvider RoleProvider { get; }

    public IAccessRoleManager RoleManager { get; }

    public UserAccountService(IUserAccountProvider provider, IAccessRoleProvider roleProvider, IAccessRoleManager roleManager, IUserAuthProvider auth, IUserAccountManager manager)
    {
        RoleProvider = roleProvider;
        RoleManager = roleManager;
        UserProvider = provider;
        Auth = auth;
        UserManager = manager;
    }
}

