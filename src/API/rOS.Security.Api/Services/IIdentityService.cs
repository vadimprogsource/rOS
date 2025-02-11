using rOS.Security.Api.Providers;
using rOS.Security.Api.Storages;
using System;
using System.Collections.Generic;
using System.Text;
using rOS.Security.Api.Configs;

namespace rOS.Security.Api.Services;

public interface IIdentityService
{
    ISecurityTokenProvider TokenProvider  { get; }
    ISecurityTokenManager  TokenManager   { get; }
    ISessionStorage        SessionStorage { get; }
    IIdentityServiceConfig Config         { get; }


}
