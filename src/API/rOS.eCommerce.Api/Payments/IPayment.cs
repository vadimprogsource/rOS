using System;
using rOS.Core.Api;
using rOS.Core.Api.Business;

namespace rOS.eCommerce.Api.Payments;

public interface IPayment : IDocument
{
    DateTime PayDateTime { get; }

    IBusinessEntity Payer { get; }
    IBusinessEntity Payee { get; }

    ICurrency Currency { get; }
    decimal Amount { get; }

}
