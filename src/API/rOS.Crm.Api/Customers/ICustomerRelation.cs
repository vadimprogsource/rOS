using System;
namespace rOS.Crm.Api.Customers;

public interface ICustomerRelation : ICustomerContact
{
    ICustomerCase Customer { get; }
    ICustomerCase Related  { get; }
}

