using rOS.Core.Api;

namespace rOS.Security.Api.Tokens;

public interface ISecurityRefreshToken : IValidator
{
    string RefreshToken { get; }
    string AccessToken { get; }
}

