using System;

namespace rOS.Core.Api;

public interface IEntity
{
    Guid Guid { get; }
}

public interface IOwneredEntity
{
    Guid? OwnerGuid { get; }
}

public interface IReadOnlyEntity
{
    DateTime CreatedOn    { get; }
    Guid     CreatorGuid  { get; }
}

public interface IReadWriteEntity
{
    DateTime? ModifiedOn   { get; }
    Guid?     ModifierGuid { get; }
}


