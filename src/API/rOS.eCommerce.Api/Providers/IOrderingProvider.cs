using rOS.eCommerce.Api.Ordering;

namespace rOS.eCommerce.Api.Providers;

public interface IOrderingProvider
{
    Task<ISalesOrder> GetOrderAsync(Guid orderGuid);
    Task<IEnumerable<ISalesOrder>> GetActiveOrdersAsync(Guid userGuid);
}
