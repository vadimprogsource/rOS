using System;
using rOS.Security.Api.Configs;
using rOS.Security.Api.Providers;
using rOS.Security.Api.Services;
using rOS.Security.Api.Storages;
using rOS.Security.Entity.Configs;

namespace rOS.Sts.Core;

public class IdentityService : IIdentityService
{
    public ISecurityTokenProvider TokenProvider { get; set; }

    public ISecurityTokenManager TokenManager { get; set; }

    public ISessionStorage SessionStorage { get; set; }

    public IIdentityServiceConfig Config { get; set; }

    public IdentityService(ISecurityTokenProvider tokenProvider, ISecurityTokenManager tokenManager, ISessionStorage sessionStorage)
    {
        TokenProvider  = tokenProvider;
        TokenManager   = tokenManager;
        SessionStorage = sessionStorage;


        Config = new IdentityServiceConfig
        {
            DefaultTokenTimeout = TimeSpan.FromMinutes(20),
            RefreshTokenTimeout = TimeSpan.FromHours(30),
            AccessTokenTimeout = TimeSpan.FromMinutes(20)
        };
    }

}
