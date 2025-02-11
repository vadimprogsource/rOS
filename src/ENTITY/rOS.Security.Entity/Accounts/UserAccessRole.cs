using rOS.Security.Api;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using rOS.Core.Entity;
using rOS.Core.Api;
using Oql.Api.Data;
using rOS.Security.Api.Permissions;
using rOS.Security.Entity.Permissions;

namespace rOS.Security.Entity.Accounts
{
    public abstract class UserAccessRole :EntityBase, IAccessRole,IOwneredEntity
    {

        public abstract DateTime CreatedOn { get; set; }
        public abstract Guid?    OwnerGuid { get; set; }



        public override Task<bool> OnCreate(IDataContext context)
        {
            CreatedOn = DateTime.Now;
            return base.OnCreate(context);
        }


        public abstract AccessRoleType RoleType { get; set; }


        public abstract string? Title { get; set; }

        public bool IsAdminRole => RoleType < AccessRoleType.SuperVisor;



        public AccessObject[] AccessObjects { get; set; } = Array.Empty<AccessObject>();

        public bool IsSupervisorRole => RoleType == AccessRoleType.SuperVisor;

        public bool IsPowerUserRole => RoleType < AccessRoleType.Admin;

        public bool IsUserRole => RoleType < AccessRoleType.PowerUser;

        public bool IsGuestRole => RoleType < AccessRoleType.User;

        public bool IsSystemRole { get; set; }

   
        public IAccessRestrictArea? RestrictArea { get; set; }

        public string Permission { get; set; } = string.Empty;

        IAccessObject[] IAccessRole.AccessObjects => AccessObjects;

        public PermissionAction[] Permissions { get; set; } = Array.Empty<PermissionAction>();

        IPermissionAction[] IAccessRole.Permissions => Permissions;

       

        public bool HasGrantedTo(IAccessRole role ,Guid? restrictAreaGuid)
        {
            bool flag = Guid == role.Guid;

            if (restrictAreaGuid.HasValue && RestrictArea != null)
            {
                flag &= restrictAreaGuid == RestrictArea.Guid;
            }

            return flag;
        }


    }
}
