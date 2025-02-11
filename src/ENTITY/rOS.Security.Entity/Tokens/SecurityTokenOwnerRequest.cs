using System;
using rOS.Security.Api;
using rOS.Security.Api.Accounts;
using rOS.Security.Api.Tokens;

namespace rOS.Security.Entity;

public readonly struct SecurityTokenOwnerRequest : ISecurityTokenOwnerRequest
{

    private readonly IUserAccount _user;

    public SecurityTokenOwnerRequest(IUserAccount user)
    {
        _user = user;
    }

    public string Role => _user.GrandedAccessRoles.Any()?( _user.GrandedAccessRoles.First().ToString() ?? "Guest"):"Guest";

    public string Owner => _user.Guid.ToString("N");

    public string TypeCode => "A";
}

