using System;
using rOS.Core.Api;

namespace rOS.eCommerce.Api.Ordering;

public interface IOrderHeader :IEntity , IDocument
{
    DateTime OrderedAt { get; }
    string OrderNumber { get; }
    string Description { get; }
    ICurrency Currency { get; }
    decimal TotalDue { get; }
    decimal SubTotal { get; }
}
