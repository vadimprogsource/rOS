using rOS.Core.Api;

namespace rOS.eCommerce.Api.Production;

public interface IProduct : IEntity , INamed , IMetaPromotion
{

    ICatalog Catalog   { get; }
    ICategory Category { get; }

    bool Visible { get; }

    string ProductCode { get; }

    ICurrency Currency { get; }
    decimal ListPrice { get; }
    bool HasPrice { get; }

    IUnitMeasure BaseUnit { get; }
}
