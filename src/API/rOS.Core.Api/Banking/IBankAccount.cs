using rOS.Core.Api;
using rOS.Core.Api.Business;

namespace rOS.Core.Api.Banking;

public interface IBankAccount : IEntity
{
    IBank Bank { get; }
    IBusinessEntity Holder { get; }
    string IBAN { get; }
    ICurrency Currency { get; }
    bool IsCardAccount { get; }
}
