using System;
using rOS.Core.Entity;
using rOS.Security.Api;
using rOS.Security.Api.Permissions;

namespace rOS.Security.Entity.Permissions
{
    public abstract class AccessRole : EntityBase, IAccessRole
    {
        
    
        public  IAccessRestrictArea? RestrictArea { get; set; }

        public abstract AccessRoleType RoleType { get; set; }

        public bool IsSupervisorRole { get; set; }

        public bool IsAdminRole { get; set; }

        public bool IsPowerUserRole { get; set; }

        public bool IsUserRole  {get;set;}

        public bool IsGuestRole { get; set; }

        public bool IsBankClient { get; set; }
        public bool IsSystemRole { get; set; }

        public ICollection<AccessObject> AccessObjects = new List<AccessObject>();



        public abstract string Title { get; set; }

        IAccessObject[] IAccessRole.AccessObjects => AccessObjects.ToArray();

        public abstract string Permission { get; set; }

        public PermissionAction[] Permissions { get; set; } = Array.Empty<PermissionAction>();

        IPermissionAction[] IAccessRole.Permissions => throw new NotImplementedException();
    }
}

