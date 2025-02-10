
using rOS.Core.Api;

namespace rOS.Core.Api.Banking;

public interface IBank : ITitled
{
    string BIC { get; }
    IAddress Address { get; }
}
