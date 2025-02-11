using System;
using rOS.Core.Api;

namespace rOS.Security.Api.Tokens;

public interface ISecurityTokenOwnerRequest : ITyped
{
    string Role { get; }
    string Owner { get; }
}

