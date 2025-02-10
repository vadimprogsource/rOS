using rOS.Core.Api;

namespace rOS.eCommerce.Api.Production;

public interface ICategory : IEntity , INamed
{
    IProduct[] Products { get; }
}
