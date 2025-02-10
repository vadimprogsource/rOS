using System;
using System.Collections.Generic;
using System.Text;
using rOS.Core.Api;

namespace rOS.eCommerce.Api.Delivery;

public interface IDeliveryNote
{
    DateTime DateTimeOn { get; }
    IAddress Address { get; }
}
