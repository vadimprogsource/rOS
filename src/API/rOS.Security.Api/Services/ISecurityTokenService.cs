using rOS.Security.Api.Providers;
using rOS.Security.Api.Tokens;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace rOS.Security.Api.Services;

public interface ISecurityTokenService 
{
    Task<ISecurityRefreshToken> GenerateRefreshTokenAsync(ISecurityTokenOwnerRequest request);
    Task<ISecurityToken> GenerateTokenAsync(ISecurityTokenOwnerRequest request);
    Task<ISecurityRefreshToken> GenerateAccessTokenAsync (ISecurityTokenRequest request);
    Task<bool> IsValidAsync(ISecurityTokenRequest request);
    Task<ISecurityTokenInfo> GetTokenInfo(ISecurityTokenRequest request);

    Task ReleaseTokenAsync        (ISecurityTokenRequest request);
    Task ReleaseTokenForOwnerAsync(ISecurityTokenOwnerRequest request);

}
