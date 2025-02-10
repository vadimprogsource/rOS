using System;
namespace rOS.Core.Api.Accounting;

public interface IAccount : IEntity , ICoded,INamed , ITitled
{
    int Level { get; }
    bool IsActive { get; }
    bool IsOutOfBalance { get; }
    string Description { get; }
}

