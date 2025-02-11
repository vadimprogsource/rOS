using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;
using rOS.Security.Api.Providers;
using rOS.Security.Api.Services;
using rOS.Sts.Apl;

namespace rOS.Sts.Core;

public static class IdentityServiceCollectionExtensions
{
    public static IServiceCollection AddIdentityService(this IServiceCollection @this)
    {
        return @this.AddScoped<ISecurityTokenManager , SecurityTokenManager>()
        .AddScoped<ISecurityTokenProvider, SecurityTokenProvider>()
        .AddScoped<IIdentityService, IdentityService>()
        .AddScoped<ISecurityTokenService, SecurityTokenService>();
    }
}
