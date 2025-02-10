using rOS.Core.Api;
using rOS.Crm.Api.Customers;
using rOS.Crm.Api.Tasks;

namespace rOS.Crm.Api.Objects;

public interface IServicedObject : ITitled , IEntity
{

    ICustomerCase Customer { get; }

    IAddress Address { get; }
    string   PhoneNumbers { get; }
    decimal  AreaSquare { get; }
    decimal  TradeAreaSquare { get; }
    decimal  SlabHeight { get; }
    decimal  CeilingHeight { get; }

    DateTime StartOfService { get; }
    DateTime EndOfService { get; }

    IReadOnlyCollection<ICustomerCase> RelatedCustomers { get; }
    IReadOnlyCollection<ITaskRequest>  Tasks { get; }
}

