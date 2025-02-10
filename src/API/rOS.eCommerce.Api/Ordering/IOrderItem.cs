using rOS.eCommerce.Api.Production;
namespace rOS.eCommerce.Api.Ordering;

public interface IOrderItem
{
    IProduct Product { get; }
    IUnitMeasure Unit { get; }
    decimal UnitPrice { get; }
    decimal Qty { get; }
    decimal TotalPrice { get; }
}
