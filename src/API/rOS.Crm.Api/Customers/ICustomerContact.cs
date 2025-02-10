using rOS.Core.Api;
using rOS.Core.Api.Personalize;

namespace rOS.Crm.Api.Customers;

public interface ICustomerContact : IPerson
{
    IReadOnlyCollection<ICustomerContactPhone> Phones { get; }
}

