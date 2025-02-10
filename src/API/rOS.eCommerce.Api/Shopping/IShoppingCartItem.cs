using rOS.Core.Api.Business;
using rOS.eCommerce.Api.Production;

namespace rOS.eCommerce.Api.Shopping;

public interface IShoppingCartItem
{
    IBusinessEntity Vendor { get; }
    IProduct Product { get; }
    decimal UnitPrice { get; }
    decimal Discount { get; }
    decimal Quantity { get; }
    decimal TotalPrice { get; }
}
