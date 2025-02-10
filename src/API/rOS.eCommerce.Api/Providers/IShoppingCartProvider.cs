using rOS.eCommerce.Api.Shopping;

namespace rOS.eCommerce.Api.Providers;

public interface IShoppingCartProvider
{
    Task<IShoppingCart> GetCartAsync(Guid userGuid);

}
