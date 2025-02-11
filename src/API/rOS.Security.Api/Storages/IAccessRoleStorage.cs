using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using rOS.Security.Api.Permissions;

namespace rOS.Security.Api.Storages;

public interface IAccessRoleStorage
{
    Task<IAccessRole>            GetRoleAsync(Guid guid);
    Task<IAccessRole[]>          GetRolesAsync(Guid? restrictAreaGuid = null);
 
    Task<bool> PutRoleAsync(IAccessRole role);
    Task<bool>        DeleteRoleAsync(IAccessRole role);
}
