using System;
using rOS.Security.Api.Permissions;
using rOS.Security.Api.Services;

namespace rOS.Uac.Core
{
    public class AccessRoleManager : IAccessRoleManager
    {
        public AccessRoleManager()
        {
        }

        public Task<IAccessRole> AddNewRole(string title, AccessRoleType roleType, IAccessObject[] accessObjects)
        {
            throw new NotImplementedException();
        }

        public Task<IAccessRole> AddObjectsToRole(IAccessRole accessRole, params IAccessObject[] accessObjects)
        {
            throw new NotImplementedException();
        }

        public Task<IAccessRole> RemoveObjectsFromRole(IAccessRole accessRole, params IAccessObject[] accessObjects)
        {
            throw new NotImplementedException();
        }

        public Task<bool> RemoveRole(IAccessRole accessRole)
        {
            throw new NotImplementedException();
        }
    }
}

