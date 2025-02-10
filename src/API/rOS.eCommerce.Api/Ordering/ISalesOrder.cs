using rOS.Core.Api;
using rOS.Core.Api.Business;

namespace rOS.eCommerce.Api.Ordering;

public interface ISalesOrder : IOrderHeader , IEntity
{
 
    IBusinessEntity Vendor { get; }
    IBusinessEntity Customer { get; }
    IOrderItem[] Items { get; }
}
