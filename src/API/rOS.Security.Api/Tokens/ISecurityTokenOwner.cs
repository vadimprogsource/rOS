using System;
using System.Collections.Generic;
using System.Text;
using rOS.Core.Api;
using rOS.Security.Api.Permissions;

namespace rOS.Security.Api.Tokens;

public interface ISecurityTokenOwner : IEntity
{
    public AccessRoleType Role { get; }
}
