using rOS.Core.Api.Orgs;

namespace rOS.Core.Api.Personalize;

public interface IUserProfile  : IDocument , INamed , ITitled,IContact
{
    IPerson        Person { get; }
    IBusinessOrg[] Orgs { get; }
}
