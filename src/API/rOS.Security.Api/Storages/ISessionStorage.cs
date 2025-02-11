using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using rOS.Security.Api.Tokens;

namespace rOS.Security.Api.Storages;

public interface ISessionStorage : ISecurityStorage
{
    Task<IDictionary<string,object>> GetDataAsync(ISecurityToken securityToken);
    Task                             PutDataAsync(ISecurityToken securityToken, IDictionary<string,object> data);
    Task DeleteDataAsync(ISecurityToken securityToken);
  
}
