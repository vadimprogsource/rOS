using System;
using System.Reflection;

namespace Oql.Api.Runtime
{
    public interface IEntityModelProperty
    {
        PropertyInfo Model { get; }
        PropertyInfo Entity { get; }
    }
}

