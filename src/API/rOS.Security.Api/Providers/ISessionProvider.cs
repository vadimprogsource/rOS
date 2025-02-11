using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using rOS.Security.Api.Sessions;
using rOS.Security.Api.Tokens;

namespace rOS.Security.Api.Providers;

public interface ISessionProvider
{
    Task<ISessionBag> GetSession(ISecurityToken token);
}
