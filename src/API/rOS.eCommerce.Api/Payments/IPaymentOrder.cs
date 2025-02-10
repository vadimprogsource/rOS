using System;
using rOS.Core.Api;
using rOS.Core.Api.Business;

namespace rOS.eCommerce.Api.Payments;

public interface IPaymentOrder : IPayment
{
    IEnumerable<IPaymentItem> Payments { get; }
}

