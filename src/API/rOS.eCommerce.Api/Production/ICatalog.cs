using rOS.Core.Api;

namespace rOS.eCommerce.Api.Production;

public interface ICatalog : IEntity , INamed , IMetaPromotion
{
    bool Visible { get; }
    ICategory[] Categories { get; }
}
