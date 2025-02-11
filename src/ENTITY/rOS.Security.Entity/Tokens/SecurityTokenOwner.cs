using System;
using System.Collections.Generic;
using System.Text;
using rOS.Security.Api;
using rOS.Security.Api.Permissions;
using rOS.Security.Api.Tokens;

namespace rOS.Security.Entity;

public class SecurityTokenOwner : ISecurityTokenOwner
{
    public Guid           Guid  { get; set; }
    public AccessRoleType Role { get; set; }


    public SecurityTokenOwner() { }
    public SecurityTokenOwner(ISecurityTokenOwner owner) 
    {
        Guid  = owner.Guid;
        Role = owner.Role;
    }



    public override string ToString() => Guid.ToString("N");
 

}
