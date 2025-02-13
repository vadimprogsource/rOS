using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using rOS.Security.Api.Accounts;

namespace rOS.Uac.Wapi.Models;

public class UserLoginModel  : IUserLogin
{
    public string Name { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
}
