using System;
using rOS.Core.Api;

namespace rOS.Security.Api.Tokens;

public interface ISecurityTokenRequest : ITyped
{
   string Token { get; }
}

