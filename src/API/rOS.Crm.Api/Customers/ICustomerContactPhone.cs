using rOS.Core.Api;

namespace rOS.Crm.Api.Customers;

public interface ICustomerContactPhone : IEntity
{
    string Phone    { get; }
    string Cellular { get; }
}

