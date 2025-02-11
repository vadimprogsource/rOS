using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using rOS.Security.Api.Permissions;
using rOS.Security.Api.Tokens;

namespace rOS.Security.Api.Services;

public interface ISecutityTokenManager
{
    Task<ISecurityToken> GenerateTokenAsync(AccessRoleType ownerRole, Guid ownerGuid,string typeCode,TimeSpan expired);
    Task<bool>           ReleaseTokenAsync (ISecurityToken token);
    Task<bool>           ReleaseTokensForGuidAsync(Guid ownerGuid);
}
