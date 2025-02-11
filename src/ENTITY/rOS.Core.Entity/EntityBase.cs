using Oql.Api.Attributes;
using Oql.Api.Data;
using rOS.Core.Api;

namespace rOS.Core.Entity;

public abstract class EntityBase : IEntity,IDataEntity , IEquatable<EntityBase>
{
    [Identity]
    public abstract Guid Guid { get; set; }

    public EntityBase() { }



    public Task OnCreateInstance(IDataContext context)
    {
        Guid = Guid.NewGuid();
        return Task.CompletedTask;
    }


    public virtual Task OnLookup(IDataContext context) => Task.CompletedTask;


    public virtual Task<bool> OnCreate(IDataContext context)
    {
        if (Guid == Guid.Empty) Guid = Guid.NewGuid();
        return Task.FromResult(true);
    }


    public virtual Task OnCreateComplete(IDataContext context) => Task.CompletedTask;
 
    public virtual Task<bool> OnUpdate(IDataContext context) => Task.FromResult(true);


    public virtual Task OnUpdateComplete(IDataContext context) => Task.CompletedTask;


    public virtual Task<bool> OnRemove(IDataContext context) => Task.FromResult(true);


    public virtual Task OnRemoveComplete(IDataContext context) => Task.CompletedTask;

    public bool Equals(EntityBase? other) => other != null && other.Guid == Guid;

    public override int GetHashCode() =>  Guid.GetHashCode();

    public override bool Equals(object? obj) => obj is IEntity x ? x.Guid == Guid : false;
    

}
