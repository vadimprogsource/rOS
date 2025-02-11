using rOS.Security.Api.Permissions;
using rOS.Security.Api.Tokens;

namespace rOS.Security.Entity;

public class SecurityToken : AccessSecurityToken
{

    public static readonly ISecurityToken Empty = new SecurityToken();



    public override  Guid OwnerGuid { get; }

    public override  string TypeCode { get;  } = "A";
    public override DateTime CreatedOn { get; set; }
    public override DateTime ExpiredOn { get;  }

    public override AccessRoleType AccessRoleType { get; set; }





    public override Guid Guid { get; set; }
    public override string Claims { get; set; } = string.Empty;
    public override Guid CreatorGuid { get; set; }

    private SecurityToken() {  }


    public SecurityToken(ISecurityTokenOwner owner, string typeCode, TimeSpan expired)
    {
        Guid  = Guid.NewGuid();

        CreatedOn = DateTime.Now;
        ExpiredOn = DateTime.Now.Add(expired);

        TypeCode = typeCode;

        OwnerGuid = owner.Guid;

        AccessRoleType = owner.Role;
 
    }


    public SecurityToken(ISecurityToken securityToken, string typeCode, TimeSpan expired) : this(securityToken.Owner, typeCode, expired)
    {
        Guid = securityToken.Guid;
        OwnerGuid = securityToken.Owner.Guid;
    }

    

}
