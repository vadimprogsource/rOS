using rOS.Core.Api.Banking;

namespace rOS.Core.Api.Orgs;

public interface IBusinessOrg : ILegalEntity 
{
    IReadOnlyCollection<IBusinessUnit> Units   { get; }
    IReadOnlyCollection<IBankAccount>  Accounts { get; }
}
