using rOS.eCommerce.Api.Production;

namespace rOS.eCommerce.Api.Pricing;

public interface IPriceListItem
{
    IProduct Product { get; }
    decimal ListPrice { get; }
}
