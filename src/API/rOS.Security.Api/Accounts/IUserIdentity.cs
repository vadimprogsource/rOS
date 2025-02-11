using rOS.Core.Api;

namespace rOS.Security.Api.Accounts;

public interface IUserIdentity : IEntity,IEquatable<IUserIdentity>
{
    string Login { get; }
}