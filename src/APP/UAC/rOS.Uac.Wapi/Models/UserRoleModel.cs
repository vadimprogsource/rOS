using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace rOS.Uac.Wapi.Models;

public class UserRoleModel
{
    public Guid   AccessRoleGuid   { get; set; }
    public Guid?  RestrictAreaGuid { get; set; }
}
