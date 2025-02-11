using rOS.Core.Entity;
using rOS.Security.Api.Permissions;

namespace rOS.Security.Entity.Permissions
{
    public abstract class AccessRestrictArea :EntityBase, IAccessRestrictArea
    {
    

        public abstract AccessRole[] Roles { get; set; }



        public abstract string Title { get; set; } 

        IAccessRole[] IAccessRestrictArea.Roles => Roles;
    }
}

