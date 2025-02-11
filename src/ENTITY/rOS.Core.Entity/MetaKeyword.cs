using rOS.Core.Api;

namespace rOS.Core.Entity;

public class MetaKeyword : IMetaKeyword
{
    public string KeyValue { get; set; } = string.Empty;

    public MetaKeyword() { }
    public MetaKeyword(IMetaKeyword key) { KeyValue = key.KeyValue; }

}
