using System;
using Oql.Api.Data;
using rOS.Core.Api;

namespace rOS.Core.Entity;

public abstract class ReadOnlyEntityBase : EntityBase,IReadOnlyEntity
{
    public abstract Guid CreatorGuid { get; set; }
    public abstract DateTime  CreatedOn  { get; set; }

    public override Task<bool> OnCreate(IDataContext context)
    {
        CreatedOn = DateTime.Now;
        return base.OnCreate(context);
    }


}

public abstract class ReadWriteEntityBase : ReadOnlyEntityBase,IReadWriteEntity
{
    public abstract Guid? ModifierGuid { get; set; }
    public abstract DateTime? ModifiedOn { get; set; }

    public override Task<bool> OnUpdate(IDataContext context)
    {
        ModifiedOn = DateTime.Now;
        return base.OnUpdate(context);
    }
}


public abstract class LoggedEntityBase : ReadWriteEntityBase , IOwneredEntity
{
    public abstract Guid? OwnerGuid { get; set; }
}

