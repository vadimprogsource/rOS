using System;
using rOS.Security.Api.Permissions;

namespace rOS.Security.Api.Accounts;

public interface IUserAccount :IUserIdentity
{
    DateTime CreatedOn { get; }
    string? Cellular { get; }
    string? Email { get; }
    string? Title { get; }
    IAccessRole Role { get; }
    IAccessRole[] GrandedAccessRoles { get; }
    bool Blocked { get; }
    bool IsValid { get; }
}
