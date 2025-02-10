using System;
using rOS.Core.Api.Business;

namespace rOS.Core.Api.Meta;

public interface IMetaCategory : IBusinessEntity , ICoded
{
    IMetaProduct[] Products { get; }
}

