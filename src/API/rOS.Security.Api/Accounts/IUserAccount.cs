using rOS.Security.Api.Permissions;

namespace rOS.Security.Api.Accounts;

public interface IUserAccount :IUserIdentity
{

    string? Cellular { get; }
    string? Email { get; }
    IAccessRole Role { get; }
    IAccessRole[] GrandedAccessRoles { get; }
    bool Blocked { get; }
    bool IsValid { get; }
}
