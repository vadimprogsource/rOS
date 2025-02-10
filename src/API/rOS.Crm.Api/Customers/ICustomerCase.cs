using rOS.Core.Api;
using rOS.Core.Api.Business;
using rOS.Core.Api.Orgs;
using rOS.Crm.Api.Objects;

namespace rOS.Crm.Api.Customers;

public interface ICustomerCase : IBusinessEntity
{
    IBusinessOrg Org { get; }
    DateTime StartOfService { get; }
    DateTime EndOfService { get; }

    ICustomerContact Contacts { get; }
    IServicedObject  ServicedObjects { get; }
}

