using System;
using rOS.Core.Api;

namespace rOS.eCommerce.Api.Payments;

public interface IPaymentItem
{
     Guid DocGuid { get; }
     ICurrency Currency { get; }
     decimal   Amount   { get; }
}

