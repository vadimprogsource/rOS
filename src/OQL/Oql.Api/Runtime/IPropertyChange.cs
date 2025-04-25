using System;
using System.Linq.Expressions;

namespace Oql.Api.Runtime;

public interface IPropertyChange
{
    string Name { get; }
    Type   BaseType { get; }

    bool   IsPrimaryKey { get; }
    bool   IsAutoInc { get; }
    int    Index { get; }
    int    Size { get; }

    Expression NewValue { get; }

}

