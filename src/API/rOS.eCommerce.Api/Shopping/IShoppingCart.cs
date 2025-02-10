using rOS.Core.Api.Business;
using rOS.eCommerce.Api.Delivery;

namespace rOS.eCommerce.Api.Shopping
{
    public interface IShoppingCart
    {
        Guid userGuid { get; }
        IBussinessUnit Unit { get; }
        IEnumerable<IShoppingCartItem> Items { get; }
        IDeliveryNote Delivery { get; }
        decimal Subtotal { get; }
        decimal TotalDue { get; }
    
    }
}
