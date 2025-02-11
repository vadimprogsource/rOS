using System;
using rOS.Core.Api;

namespace rOS.Security.Api.Tokens;

public interface ISecurityTokenInfo : ISecurityTokenRequest , ISecurityTokenOwnerRequest,IValidator
{
    DateTime ExpiredOn { get; }
}

