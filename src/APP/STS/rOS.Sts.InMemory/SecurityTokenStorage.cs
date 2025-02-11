using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using rOS.Security.Api;
using rOS.Security.Api.Storages;
using rOS.Security.Api.Tokens;
using rOS.Security.Entity;

namespace rOS.Sts.InMemory;

public class SecurityTokenStorage : Dictionary<Guid , ISecurityToken> , ISecurityTokenStorage
{


    public Task<ISecurityToken> GetTokenAsync(string token)
    {
       


        if (Guid.TryParse(token, out Guid guid))
        {
         
            if (TryGetValue(guid, out ISecurityToken? securityToken))
            {
                return Task.FromResult(securityToken);
            }
        }

        return Task.FromResult( SecurityToken.Empty);
    }

    public Task PutTokenAsync(ISecurityToken token)
    {

        this[token.Guid] = token;
        return Task.CompletedTask;
    }

 

    public Task DeleteTokenAsync(ISecurityToken token)
    {
        Remove(token.Guid);
        return Task.CompletedTask;
    }


    public Task DeleteAllAsync(ISecurityTokenOwner owner)
    {

        Guid[] removed = Values.Where  (x => x.Owner.Guid == owner.Guid)
                                       .Select (x=>x.Guid)
                                       .ToArray();

        foreach (Guid uid in removed)
        {
            Remove(uid);
        }

        return Task.CompletedTask;
    }

    public Task ForceClearExpired()
    {
        Guid[] expired = Values.Where(x => x.ExpiredOn <= DateTime.Now)
                               .Select(x => x.Guid)
                               .ToArray();

        if (expired.Length > 0)
        {
            foreach (Guid token in expired)
            {
                Remove(token);
            }
        }

        return Task.CompletedTask;
    }


}
