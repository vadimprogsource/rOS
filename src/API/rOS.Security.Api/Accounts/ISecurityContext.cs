using System.Security.Claims;
using System.Security.Principal;
using rOS.Security.Api.Permissions;
using rOS.Security.Api.Tokens;

namespace rOS.Security.Api.Accounts;

public interface ISecurityContext : IIdentity
{
 
    IUserIdentity User  { get; }
    ISecurityToken SecurityToken { get; }
    IAccessRole AccessRole { get; }

    Tuple<string, Guid> GenerateNewPassword();

    ClaimsPrincipal MakeUserProxy();
}