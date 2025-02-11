using System;
using rOS.Core.Entity;
using rOS.Security.Api;
using rOS.Security.Api.Permissions;

namespace rOS.Security.Entity.Permissions
{
    public abstract class PermissionAction :EntityBase, IPermissionAction
    {
        public abstract Guid OwnerGuid { get; set; }
        public abstract AccessObject AccessObject { get; set; }
        public abstract AccessPermission Create { get; set; }
        public abstract AccessPermission Read   { get; set; }
        public abstract AccessPermission Update { get; set; }
        public abstract AccessPermission Delete { get; set; }
    }
}

