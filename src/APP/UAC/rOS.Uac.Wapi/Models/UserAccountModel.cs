using System;
using rOS.Security.Api.Accounts;
using rOS.Security.Api.Permissions;

namespace rOS.Uac.Wapi.Models;

public record UserAccountModel 
{
    public Guid Guid { get; set; }

    public string? Login { get; set; }

    public string? Cellular { get; set; }

    public string? Email { get; set; }

    public string? Title { get; set; }

    public string? Role { get; set; }

    public bool Blocked { get; set; }
}

