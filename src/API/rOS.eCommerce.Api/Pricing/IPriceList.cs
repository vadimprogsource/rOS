using System;
using System.Collections.Generic;
using rOS.Core.Api;
using rOS.Core.Api.Business;

namespace rOS.eCommerce.Api.Pricing;

public interface IPriceList : IEntity , IDocument
{
    DateTime FromDateTime { get; }
    IBusinessEntity Vendor { get; }

    IEnumerable<IPriceListItem> Items { get; }
}
