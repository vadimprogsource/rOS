using System;
using rOS.Security.Api;
using rOS.Security.Api.Tokens;

namespace rOS.Security.Entity;

public class SecurityRefreshToken : ISecurityRefreshToken
{

    private readonly ISecurityToken m_refresh;
    private readonly ISecurityToken m_access;

    public static SecurityRefreshToken Empty => new(SecurityToken.Empty, SecurityToken.Empty);

  
    public string RefreshToken => m_refresh.Token;

    public string AccessToken => m_access.Token;

    public bool IsValid =>m_refresh.IsValid && m_access.IsValid;

    public SecurityRefreshToken(ISecurityToken refresh , ISecurityToken access)
    {
        m_refresh = refresh;
        m_access = access;
    }

}

