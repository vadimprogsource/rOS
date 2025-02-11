using rOS.Security.Api.Tokens;

namespace rOS.Security.Api.Providers;

public interface ISecurityTokenProvider 
{
    Task<ISecurityToken> GetTokenInfoAsync(string token);
    Task<bool>           IsTokenValidAsync(string token);

    Task<ISecurityTokenOwner> GetTokenOwnerAsync(string owner,string role);

}
