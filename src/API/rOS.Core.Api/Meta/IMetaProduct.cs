using System;
using rOS.Core.Api.Business;

namespace rOS.Core.Api.Meta;


public interface IMetaProductOption : ICoded, ITitled
{
}


public interface IMetaProduct : IBusinessEntity,ICoded,ITitled
{
    public IMetaProductOption[] Options { get; }
}

