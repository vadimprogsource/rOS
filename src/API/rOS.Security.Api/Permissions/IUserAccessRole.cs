using System;
using System.Collections.Generic;
using System.Text;

namespace rOS.Security.Api.Permissions;

public interface IUserAccessRole
{
    IAccessRole Role { get; }
    Guid RestrictAreaGuid { get; }
}
