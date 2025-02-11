using System;
using rOS.Security.Api.Tokens;

namespace rOS.Security.Entity;

public class SecurityTokenInfo : ISecurityTokenInfo
{
    private readonly ISecurityToken _token;

    public SecurityTokenInfo(ISecurityToken tokenInfo)
    {
        _token = tokenInfo;
    }

    public DateTime ExpiredOn => _token.ExpiredOn;

    public string Token => _token.Token;

    public string Role => _token.Owner.Role.ToString() ?? string.Empty;

    public string Owner => _token.Owner.ToString() ?? string.Empty;

    public string TypeCode => _token.TypeCode;

    public bool IsValid => _token.IsValid;
}

