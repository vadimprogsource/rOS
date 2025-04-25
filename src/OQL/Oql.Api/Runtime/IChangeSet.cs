using System;
namespace Oql.Api.Runtime;




public interface IChangeSet
{
    bool Modified { get; }
    IEnumerable<IPropertyChange> GetChanges();
    void Reset();
}

