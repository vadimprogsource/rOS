using rOS.Security.Api.Accounts;

namespace rOS.Security.Api.Providers;

public interface ISecurityContextProvider
{
    ISecurityContext GetContext();
}