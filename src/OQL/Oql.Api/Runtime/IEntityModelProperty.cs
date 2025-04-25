using System;
using System.Reflection;

namespace Oql.Api.Runtime
{
    public interface IEntityModelProperty
    {
        int Index { get; }
        bool IsIdentity { get; }
        PropertyInfo Model { get; }
        PropertyInfo Entity { get; }
    }
}

