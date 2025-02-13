using System;
using rOS.Security.Api.Permissions;
using rOS.Security.Api.Providers;
using rOS.Security.Api.Validators;

namespace rOS.Uac.Core
{
    public class AccessRoleProvider : IAccessRoleProvider
    {
        public AccessRoleProvider()
        {
        }

        public Task<IAccessRoleVaidator?> GetAllowedRole(string roleName)
        {
            throw new NotImplementedException();
        }

        public string[] GetAllowedRoles()
        {
            throw new NotImplementedException();
        }

        public Task<IAccessRole> GetRoleAsync(Guid roleGuid)
        {
            throw new NotImplementedException();
        }

        public Task<IAccessRole[]> GetRolesAsync()
        {
            throw new NotImplementedException();
        }
    }
}

