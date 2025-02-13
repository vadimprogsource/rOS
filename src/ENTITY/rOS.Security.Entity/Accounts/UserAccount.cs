using rOS.Security.Api;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using rOS.Core.Entity;
using Oql.Api.Data;
using rOS.Security.Api.Accounts;
using rOS.Security.Api.Permissions;

namespace rOS.Security.Entity.Accounts
{
    public abstract class UserAccount :UserAccessRole, IUserAccount
    {
    
        public abstract Guid PasswordGuid { get; set; }

        public abstract byte[] PasswordHash { get; set; }

        public abstract string Login { get; set; }


        public UserAccessRole[] GrandedAccessRoles { get; set; } = Array.Empty<UserAccessRole>();

       
        
        public abstract bool Blocked { get; set; }


        public bool IsValid => CreatedOn > DateTime.MinValue && Guid != Guid.Empty;

        public abstract string? Cellular { get; set; }

        public abstract string? Email { get; set; }

        IAccessRole[] IUserAccount.GrandedAccessRoles => GrandedAccessRoles;

        public IAccessRole Role => this;

   

        bool IEquatable<IUserIdentity>.Equals(IUserIdentity? identity)
        {
            return identity != null && identity.Guid == Guid;
        }


        public override Task<bool> OnCreate(IDataContext context)
        {
            CreatedOn = DateTime.Now;
            RoleType = AccessRoleType.Guest;
            Blocked = true;
            PasswordHash = new byte[] { default };
            PasswordGuid = Guid.Empty;
            return base.OnCreate(context);
        }

    }
}
