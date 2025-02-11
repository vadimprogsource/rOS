using rOS.Core.Entity;
using rOS.Security.Api.Permissions;
using rOS.Security.Api.Tokens;

namespace rOS.Security.Entity;

public abstract class AccessSecurityToken : ReadOnlyEntityBase , ISecurityToken
{
    public AccessSecurityToken()
    {
    }

    public abstract Guid OwnerGuid { get; }

    public Guid Sid => Guid;
    public abstract string TypeCode { get; } 
    public abstract DateTime ExpiredOn { get; }


    public abstract AccessRoleType AccessRoleType { get; set; }

    public bool HasExpired => ExpiredOn <= DateTime.Now;

    public bool IsValid => ExpiredOn > DateTime.Now;
    public string Token => Guid.ToString("n");
  

    public abstract string Claims { get; set; }

    public ISecurityTokenOwner Owner => new SecurityTokenOwner { Guid = OwnerGuid, Role = AccessRoleType };

    IDictionary<string, object> ISecurityToken.Claims => throw new NotImplementedException();

    public override string ToString() => Token;
    
}

