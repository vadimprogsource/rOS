using rOS.eCommerce.Api.Ordering;
using rOS.eCommerce.Api.Shopping;

namespace rOS.eCommerce.Api.Services;

public interface IOrderingService
{

    Task<IEnumerable<ISalesOrder>> CheckoutAsync(IShoppingCart cart);
}
