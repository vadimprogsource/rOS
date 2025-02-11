using System;
using rOS.Security.Api.Accounts;
using rOS.Security.Api.Tokens;

namespace rOS.Security.Api.Providers;

public interface IAuthProvider
{
    Task<ISecurityRefreshToken> SignInAsync(ISecurityLogin login);
    Task<ISecurityRefreshToken> GetAccessToken(ISecurityTokenRequest request);
    
    Task<ISecurityContext> Authorize(string? rawToken);
    
    Task SignOut(ISecurityTokenRequest request);
    
    
}

