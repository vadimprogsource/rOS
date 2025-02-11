using System;
using System.Collections.Generic;
using System.Text;

namespace rOS.Security.Api.Configs;

public interface IIdentityServiceConfig
{
    TimeSpan DefaultTokenTimeout { get; }
    TimeSpan RefreshTokenTimeout { get; }
    TimeSpan AccessTokenTimeout  { get; }
}
