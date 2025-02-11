using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using rOS.Security.Api;
using rOS.Security.Api.Permissions;
using rOS.Security.Api.Providers;
using rOS.Security.Api.Storages;
using rOS.Security.Api.Tokens;
using rOS.Security.Entity;

namespace rOS.Sts.Apl;

public class SecurityTokenProvider : ISecurityTokenProvider
{

    private readonly ISecurityTokenStorage m_storage;


    public SecurityTokenProvider(ISecurityTokenStorage storage)
    {
        m_storage = storage;
    }

    public async Task<ISecurityToken> GetTokenInfoAsync(string token)
    {

        ISecurityToken securityToken = await m_storage.GetTokenAsync(token);

        if (securityToken == null)
        {
            return SecurityToken.Empty;
        }

        if (securityToken.HasExpired)
        {
            await m_storage.DeleteTokenAsync(securityToken);
            return SecurityToken.Empty;
        }

        return securityToken;
    }

    public Task<ISecurityTokenOwner> GetTokenOwnerAsync(string owner,string role)
    {
        SecurityTokenOwner own = new()
        {
            Guid = Guid.TryParse(owner, out Guid guid)?guid:Guid.NewGuid(),
            Role = Enum.TryParse(role, out AccessRoleType roleType)? roleType : AccessRoleType.Guest
        };

        return Task.FromResult( own as ISecurityTokenOwner);
    }

    public async Task<bool> IsTokenValidAsync(string token)
    {
        ISecurityToken securityToken = await GetTokenInfoAsync(token);
        return !(securityToken == null || securityToken.HasExpired);
    }
}
