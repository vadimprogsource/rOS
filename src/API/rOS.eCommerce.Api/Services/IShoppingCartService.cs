using rOS.Core.Api.Business;
using rOS.eCommerce.Api.Production;

namespace rOS.eCommerce.Api.Shopping;

public interface IShoppingCartService
{

    Task<IShoppingCartItem> AddProductToCartAsync(IShoppingCart cart ,  IBusinessEntity vendor, IProduct product, decimal quantity);
    Task<IShoppingCartItem> RemoveProductFromCartAsync(IShoppingCart cart, IBusinessEntity vendor, IProduct product);
    Task<bool> ReleaseCartAsync(IShoppingCart cart);
}
