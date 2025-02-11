using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using rOS.Security.Api.Tokens;

namespace rOS.Security.Api.Services;

public interface ISecurityTokenManager
{
    Task<ISecurityToken> GenerateNewTokenAsync(ISecurityTokenOwner owner , string typeCode,TimeSpan expired);
    Task<ISecurityToken> GenerateNewTokenAsync(ISecurityToken token, string typeCode, TimeSpan expired);

    Task<bool>           ReleaseTokenAsync (ISecurityToken token);
    Task<bool>           ReleaseTokensForOwnerAsync(ISecurityTokenOwner  owner);
}
