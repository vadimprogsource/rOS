

using rOS.Core.Api;

namespace rOS.Core.Entity;

public abstract class Document : EntityBase, IDocument
{
    public abstract DateTime CreatedOn { get; set; }
    public abstract Guid OwnerGuid { get; set; }


    public Document() { }
 
}
