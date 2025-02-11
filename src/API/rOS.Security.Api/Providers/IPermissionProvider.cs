using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using rOS.Security.Api.Permissions;

namespace rOS.Security.Api.Providers;

public interface IPermissionProvider
{
    Task<bool> IsAllow(IAccessRole role,string uriPath, Guid? restrictAreaGuid= null);
    Task<bool> IsDeny (IAccessRole role,string uriPath, Guid? restrictAreaGuid = null);
}
