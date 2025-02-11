using System;
using System.Collections.Generic;
using System.Text;

namespace rOS.Security.Api.Permissions;

public interface IAccessObject
{
    bool IsAllow { get; }
    bool IsDeny  { get; }
    string UriPath { get; }
}
