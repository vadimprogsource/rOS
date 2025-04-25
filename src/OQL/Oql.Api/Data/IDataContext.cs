using System;
using Oql.Api.Linq;
using Oql.Api.Runtime;

namespace Oql.Api.Data;

public interface IDataContext : IScope
{
    IQueryable<T> AsQueryable<T>();

    IChangeSet ChangeSet { get; }
}
