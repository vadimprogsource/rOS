using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using rOS.Security.Api;
using rOS.Security.Api.Services;
using rOS.Security.Api.Storages;
using rOS.Security.Api.Tokens;
using rOS.Security.Entity;

namespace rOS.Sts.Core;

public class SecurityTokenManager : ISecurityTokenManager
{

    private readonly ISecurityTokenStorage _storage;

    public SecurityTokenManager(ISecurityTokenStorage storage)
    {
        _storage = storage;
    }

 

    public async Task<ISecurityToken> GenerateNewTokenAsync(ISecurityTokenOwner owner, string typeCode, TimeSpan expired)
    {
        ISecurityToken token = new SecurityToken(owner, typeCode, expired);
        await _storage.PutTokenAsync(token);
        return token;
    }

    public async Task<ISecurityToken> GenerateNewTokenAsync(ISecurityToken token, string typeCode, TimeSpan expired)
    {
        ISecurityToken new_token = new SecurityToken(token, typeCode, expired);
        await _storage.PutTokenAsync(new_token);
        return new_token;
    }
  
    public async Task<bool> ReleaseTokenAsync(ISecurityToken token)
    {
        await _storage.DeleteTokenAsync(token);
        return true;
    }

    public async Task<bool> ReleaseTokensForOwnerAsync(ISecurityTokenOwner owner)
    {
       await _storage.DeleteAllAsync(owner);
       return true;
    }
}
