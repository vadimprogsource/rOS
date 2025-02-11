using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using rOS.Security.Api.Tokens;

namespace rOS.Security.Api.Storages;

public interface ISecurityTokenStorage : ISecurityStorage
{
    Task<ISecurityToken> GetTokenAsync   (string token);
    Task                 PutTokenAsync   (ISecurityToken token);
    Task                 DeleteTokenAsync(ISecurityToken token);
    Task                 DeleteAllAsync  (ISecurityTokenOwner owner);
}
