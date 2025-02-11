using System;
using System.Collections.Generic;
using System.Text;
using rOS.Security.Api.Configs;

namespace rOS.Security.Entity.Configs;

public class IdentityServiceConfig : IIdentityServiceConfig
{
    public TimeSpan DefaultTokenTimeout { get; set; }

    public TimeSpan RefreshTokenTimeout { get; set; }

    public TimeSpan AccessTokenTimeout { get; set; }
}
