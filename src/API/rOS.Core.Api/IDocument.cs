 using System;
using System.Collections.Generic;
using System.Text;

namespace rOS.Core.Api;

public interface IDocument : IEntity
{
    DateTime CreatedOn { get; }
    Guid     OwnerGuid   { get; }
}
