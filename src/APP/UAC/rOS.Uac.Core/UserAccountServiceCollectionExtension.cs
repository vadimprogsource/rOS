using System;
using Microsoft.Extensions.DependencyInjection;
using rOS.Security.Api.Providers;
using rOS.Security.Api.Services;

namespace rOS.Uac.Core;

public static class UserAccountServiceCollectionExtension
{
    public static IServiceCollection AddUserAccountService(this IServiceCollection @this)
    {
        return @this.AddScoped<IAccessRoleProvider, AccessRoleProvider>()
               .AddScoped<IUserAccountProvider, UserProvider>()
               .AddScoped<IAccessRoleManager, AccessRoleManager>()
               .AddScoped<IUserAccountManager, UserAccountManager>()
               .AddScoped<IUserAuthProvider, UserAuthProvider>()
               .AddScoped<IUserAccountService, UserAccountService>();
    }
}

