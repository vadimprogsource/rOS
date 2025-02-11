using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using rOS.Security.Api.Permissions;

namespace rOS.Security.Api.Services;

public interface IAccessRoleManager
{
    Task<IAccessRole> AddNewRole            (string title, AccessRoleType roleType, IAccessObject[] accessObjects);
    Task<IAccessRole> AddObjectsToRole     (IAccessRole accessRole, params IAccessObject[] accessObjects);
    Task<IAccessRole> RemoveObjectsFromRole(IAccessRole accessRole, params IAccessObject[] accessObjects);
    Task<bool> RemoveRole(IAccessRole accessRole);
}
