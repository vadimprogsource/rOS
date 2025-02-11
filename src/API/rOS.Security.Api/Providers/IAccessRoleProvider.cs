using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using rOS.Security.Api.Permissions;
using rOS.Security.Api.Validators;

namespace rOS.Security.Api.Providers;

public interface IAccessRoleProvider
{
    Task<IAccessRole[]> GetRolesAsync();
    Task<IAccessRole> GetRoleAsync(Guid roleGuid);
    
    
    Task<IAccessRoleVaidator?> GetAllowedRole(string roleName);

    string[] GetAllowedRoles();
    
}
