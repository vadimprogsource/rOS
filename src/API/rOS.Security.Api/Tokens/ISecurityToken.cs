using System;
using System.Collections.Generic;
using System.Text;
using rOS.Core.Api;

namespace rOS.Security.Api.Tokens;

public interface ISecurityToken : IEntity
{
    Guid     Sid              { get; }
    string   TypeCode         { get; }
    DateTime CreatedOn        { get; }
    DateTime ExpiredOn        { get; }
    ISecurityTokenOwner Owner { get; }
    bool HasExpired           { get; }
    bool IsValid              { get; }
    string Token              { get; }

    IDictionary<string, object> Claims { get; }
}
